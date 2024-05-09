using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartService.Controllers{

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