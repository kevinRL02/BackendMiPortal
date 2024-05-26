using ShoppingCartService.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShoppingCartService.SyncDataServices.Http
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UserServiceClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var url = $"{_configuration["UserService"]}{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            return null;
        }

        public async Task<Products?> GetProductByIdAsync(int id)
        {
            var url = $"{_configuration["ProductsService"]}{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonString); // Log the JSON response

                // Parse the JSON string
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = doc.RootElement;

                    // Clean the JSON manually
                    var cleanedJson = new
                    {
                        id = root.GetProperty("id").GetInt32(),
                        name = root.GetProperty("name").GetString(),
                        productCategoryId = root.GetProperty("productCategoryId").GetInt32(),
                        description = root.GetProperty("description").GetString(),
                        unitCost = root.GetProperty("unitCost").GetDouble(),
                        stock = root.GetProperty("stock").GetInt32(),
                        reorderPoint = root.GetProperty("reorderPoint").GetInt32(),
                        productRatings = root.GetProperty("productRatings").GetProperty("$values").EnumerateArray(),
                        productCategory = root.GetProperty("productCategory").GetRawText(),
                        supplierProductsOrders = root.GetProperty("supplierProductsOrders").GetProperty("$values").EnumerateArray()
                    };

                    // Convert the cleaned object to JSON string
                    string cleanedJsonString = JsonSerializer.Serialize(cleanedJson);

                    // Define JsonSerializer options
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                    };

                    // Deserialize the cleaned JSON string to Products object
                    return JsonSerializer.Deserialize<Products>(cleanedJsonString, options);
                }
            }
            return null;
        }

        
    }
}
