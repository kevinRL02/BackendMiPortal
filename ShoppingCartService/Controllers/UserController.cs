using Microsoft.AspNetCore.Mvc;
 
namespace ShoppingCartService.Controllers{


//Acá se gestiona la comunicación entre servicios
    [Route("api/c/[controller]")]
    [ApiController]
    public class UserController : ControllerBase{

        public UserController(){

        }

        [HttpPost]
        public ActionResult TestConnection(){
            Console.WriteLine("PostShoppingCartervice");
            return Ok("Testo from UserService");
        }

    }
}