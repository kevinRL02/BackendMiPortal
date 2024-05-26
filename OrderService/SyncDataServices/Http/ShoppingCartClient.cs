using OrderService.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrderService.SyncDataServices.Http
{
    public class ShoppingCartClient : IShoppingCartClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ShoppingCartClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ShoppingCart> GetShoppingCartByUserId(int userId)
        {
            var response = await _httpClient.GetStringAsync($"{_configuration["ShoppingCartService"]}user/{userId}");
            var shoppingCarts = JsonConvert.DeserializeObject<List<ShoppingCart>>(ExtractValues(response));

            if (shoppingCarts == null || shoppingCarts.Count == 0)
            {
                return null; // O maneja el caso de no encontrar carritos según tu lógica de negocio
            }

            var shoppingCart = shoppingCarts.First();
            return shoppingCart;
        }

        public async Task<List<ItemsShoppingCart>> GetItemsByShoppingCartId(int cartId)
        {
            var response = await _httpClient.GetStringAsync($"{_configuration["ItemsShoppingCartService"]}cart/{cartId}");
            return JsonConvert.DeserializeObject<List<ItemsShoppingCart>>(ExtractValues(response));
        }

        public async Task ClearShoppingCart(int cartId)
        {
            await _httpClient.DeleteAsync($"{_configuration["ItemsShoppingCartService"]}cart/{cartId}");
        }

        private string ExtractValues(string jsonResponse)
        {
            var jObject = JObject.Parse(jsonResponse);
            var values = jObject["$values"];
            return values.ToString();
        }
    }
}
