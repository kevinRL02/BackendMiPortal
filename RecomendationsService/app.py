from flask import Flask, request, jsonify
import pandas as pd
import requests
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import linear_kernel

app = Flask(__name__)

# Function to fetch data from an API and convert it to a DataFrame
def fetch_data_from_api(api_url):
    response = requests.get(api_url)
    response.raise_for_status()  # Raise an error for bad status codes
    data = response.json()
    return pd.DataFrame(data)

# Example API URLs (replace these with the actual URLs)
products_api_url = 'http://productservice-clusterip-srv:8080/api/products/'
categories_api_url = 'http://productservice-clusterip-srv:8080/api/productcategories'
orders_api_url = 'http://orderservice-clusterip-srv:8080/api/order/'

# Fetch data from APIs
products_df = fetch_data_from_api(products_api_url)
categories_df = fetch_data_from_api(categories_api_url)
orders_df = fetch_data_from_api(orders_api_url)

# Normalize and rename columns for consistency
products_df = pd.json_normalize(products_df['$values'])
products_df.rename(columns={'id': 'ID_Producto', 'name': 'Nombre_Producto', 'productCategoryId': 'ID_Categoria_Producto', 'description': 'Descripción'}, inplace=True)
categories_df = pd.json_normalize(categories_df['$values'])
categories_df.rename(columns={'id': 'ID_Categoria_Producto', 'name': 'Nombre_Categoria'}, inplace=True)

# Drop unnecessary columns
products_df.drop(columns=['$id'], inplace=True)
categories_df.drop(columns=['$id'], inplace=True)

# Merge products with their categories
products_with_categories = pd.merge(products_df, categories_df, left_on='ID_Categoria_Producto', right_on='ID_Categoria_Producto', how='left')

# Function to fetch items for a given order
def fetch_order_items(user_id):
    order_items_api_url = f'http://orderservice-clusterip-srv:8080/api/order/items/user/{user_id}'
    response = requests.get(order_items_api_url)
    response.raise_for_status()
    data = response.json()
    if data["$values"]:  # Check if there are order items
        order_items_df = pd.json_normalize(data["$values"])
        order_items_df.rename(columns={'productId': 'ID_Producto', 'orderId': 'ID_Orden'}, inplace=True)
        return order_items_df
    else:
        return pd.DataFrame()

# Function to get product recommendations for a user based on order items
def get_recommendations_for_user(user_id):
    order_items_df = fetch_order_items(user_id)
    if not order_items_df.empty:
        # Merge order items with products
        order_items_with_products = pd.merge(order_items_df, products_with_categories, on='ID_Producto', how='left')

        # Create a TF-IDF matrix of product descriptions
        tfidf = TfidfVectorizer(stop_words='english')
        tfidf_matrix = tfidf.fit_transform(products_with_categories['Descripción'])

        # Compute the cosine similarity matrix
        cosine_sim = linear_kernel(tfidf_matrix, tfidf_matrix)

        user_order_items = order_items_with_products['ID_Producto']
        user_product_indices = products_with_categories[products_with_categories['ID_Producto'].isin(user_order_items)].index.tolist()
        sim_scores = []
        for idx in user_product_indices:
            sim_scores.extend(list(enumerate(cosine_sim[idx])))
        sim_scores = sorted(sim_scores, key=lambda x: x[1], reverse=True)
        sim_scores = sim_scores[:3]  # Adjust the number of recommendations as needed
        product_indices = [i[0] for i in sim_scores]
        recommendations = products_with_categories.iloc[product_indices]
        return recommendations.to_dict(orient='records')
    else:
        return []

# Define a route to handle requests for recommendations
@app.route('/recommendations', methods=['GET'])
def recommendations():
    user_id = request.args.get('user_id')
    if user_id:
        recommendations = get_recommendations_for_user(int(user_id))
        return jsonify(recommendations)
    else:
        return jsonify({'error': 'User ID parameter is missing.'})

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=8080)
