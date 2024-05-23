using System.Collections.Generic;
using ProductsService.Models; // Asegúrate de que esto esté correcto

namespace ProductsService.Data{
    public interface IProductRepo{
        bool SaveChanges();

        IEnumerable<Products> GetAllProducts();

        // Obtener un producto por su ID
        Products GetProductById(int id);

        // Crear un nuevo producto
        void CreateProduct(Products product);

        // Actualizar un producto existente
        void UpdateProduct(Products product);

        // Eliminar un producto
        void DeleteProduct(Products product);

        //

        IEnumerable<ProductCategory> GetAllProductCategories();
        ProductCategory GetProductCategoryById(int id);
        void CreateProductCategory(ProductCategory productCategory);
        void UpdateProductCategory(ProductCategory productCategory);
        void DeleteProductCategory(ProductCategory productCategory);
        IEnumerable<Products> GetProductsByCategory(int categoryId);




        //


        IEnumerable<ProductRating> GetAllProductRatings();
        ProductRating GetProductRatingById(int id);
        void CreateProductRating(ProductRating productRating);
        void UpdateProductRating(ProductRating productRating);
        void DeleteProductRating(ProductRating productRating);


        //
        IEnumerable<Supplier> GetAllSuppliers();
        Supplier GetSupplierById(int id);
        void CreateSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(Supplier supplier);

        //
        IEnumerable<SupplierProductsOrder> GetAllSupplierProductsOrders();
        SupplierProductsOrder GetSupplierProductsOrderById(int id);
        void CreateSupplierProductsOrder(SupplierProductsOrder supplierProductsOrder);
        void UpdateSupplierProductsOrder(SupplierProductsOrder supplierProductsOrder);
        void DeleteSupplierProductsOrder(SupplierProductsOrder supplierProductsOrder);





    }
}