using System.Text;
using System.Text.Json;
using UserService1.Dtos;
using Microsoft.Extensions.Configuration;

//Esta clase es para la comunicación REST entre servicios

namespace UserService1.SyncDataServices.Http{
    public class HttpShopingCartDataClient(HttpClient httpCLient, IConfiguration configuration) : IShoppingCartDataClient
    {
        private readonly HttpClient _httpClient = httpCLient;
        

        //Vamos a utilizar esto para inyectarlo en un config file. QUe se llama appsetings,Devekipnebts
        private readonly IConfiguration _configuration = configuration;

        //
        async Task IShoppingCartDataClient.SendUserToShoppingCart(UserReadDto userReadDto)
        {

             // Serializar el objeto userReadDto a JSON
            var jsonUser = JsonSerializer.Serialize(userReadDto);

            // Crear el contenido HTTP con la cadena JSON
            var httpContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            // Realizar la solicitud HTTP POST al servicio del carrito de compras ayncrona
            //La direccino deberia ir en un archivo de configuracion
            //Referncia el método que esta en el código del carrito. (UserController)
            //Se referncia la dirección que esta en el config file
            var response = await _httpClient.PostAsync($"{_configuration["ShoppingService"]}/api/c/user", httpContent);
            // Manejar la respuesta aquí si es necesario
            // Por ejemplo, verificar si la solicitud fue exitosa, manejar errores, etc.

            if(response.IsSuccessStatusCode){
                Console.WriteLine("La comunicacion a el shopping cart funciona");
            }else{
                Console.WriteLine("La comunicacion a el shopping no cart funciona");
            }

        }
    }
}