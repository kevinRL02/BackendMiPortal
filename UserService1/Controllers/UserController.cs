using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using UserService1.Dtos;
using UserService1.Models;
using UserService1.SyncDataServices.Http;

namespace UserService1.Controllers{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;

        //Para la comunicación entre servicios
        private IShoppingCartDataClient _httpShopingCartDataClient;

        public UserController(IUserRepo repo , IMapper mapper ,IShoppingCartDataClient httpShopingCartDataClient ){
            _repo = repo;
            _mapper = mapper; 
            _httpShopingCartDataClient = httpShopingCartDataClient;
        }

        //Cuando llamamos esta accion se va a returnar una enumeracion del UserReadDTO, no de el User model, o del User Data
        //Cuando queremos enviar datos, lo queremos hacer a travpes del DTO representation de nuestros datos
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetUsers(){
            Console.WriteLine("Obteniendo los usuarios");
            //Llamamos la interfz de usuarios
            var userItem = _repo.GetAllUsers();

            //Ahora vamos a mapear de la colección que estamos obteniendo a través del DTO
            //Entonces acá se esta usando el metodo que definimos en el foloder Profiles
            //Este metodo CreateMap<User,UserReadDto>();
            //Mapper es lo suficientemente intelegitnte como para reconocer que metodo usar
            return Ok(_mapper.Map<IEnumerable<UserReadDto>> (userItem));

        }

        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserReadDto> GetUserById(int id){
            var userItem = _repo.GetUserById(id);
            if(userItem != null){
                //Source-Destination
                return Ok(_mapper.Map<UserReadDto> (userItem));
            }
            return NotFound();
            
        }

        //Ponermos UserReadDto, ya que vamos a decolver el ususrio si este se crea correctamente
        [HttpPost]
        public async Task< ActionResult<UserReadDto>> CreateUser(UserCreateDto userCreateDto){
            //userCreateDto es el source
            //Aca se esta usando este metodo del profile CreateMap<UserCreateDto,User>();

            /*Origen (userCreateDto): El objeto userCreateDto es un DTO (Data Transfer Object) que probablemente proviene de una solicitud HTTP, como la creación de un nuevo usuario a través de un formulario web o una solicitud de API.
            Destino (User): El objeto User es una entidad de la base de datos que representa un usuario en el sistema. Esta es la entidad que se almacenará en la base de datos después de que se haya creado a partir de los datos del DTO.
            Mapeo con AutoMapper: Cuando se llama al método Map<User>, AutoMapper utiliza la configuración definida en el perfil de mapeo (UserProfile) para realizar el mapeo de los campos del DTO al modelo de usuario.Por ejemplo, si el DTO UserCreateDto tiene propiedades como Name, Password, y Email, y el modelo User tiene propiedades correspondientes, AutoMapper mapeará automáticamente los valores del DTO a las propiedades del modelo con los mismos nombres.
            // Ejemplo de mapeo de propiedades
            userCreateDto.Name -> userModel.Name
            userCreateDto.Password -> userModel.Password
            userCreateDto.Email -> userModel.Email
            
            */
 
           var userModel = _mapper.Map<User>(userCreateDto);
           _repo.AddUser(userModel);
           _repo.saveChanges();

            //Vamos a obtenr el suaurio creado usando readDto
            var userReadDTO = _mapper.Map<UserReadDto>(userModel);
            //retorna un 201

            //Llamado a el ShoppingService

            try{
                await _httpShopingCartDataClient.SendUserToShoppingCart(userReadDTO);
            }catch(Exception ex){
                Console.WriteLine("No se puede enviar sincronicamente : {ex.Message}");
            }
            return CreatedAtRoute(nameof(UserController.GetUserById), new { Id = userReadDTO.Id }, userReadDTO);
        }
    }
}