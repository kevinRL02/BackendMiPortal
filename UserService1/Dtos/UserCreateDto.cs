namespace UserService1.Dtos{

    public class UserCreateDto{
        //Esta clase representa un objeto de transferencia de datos (DTO) utilizado para crear un nuevo usuario.
        //DTOs están diseñados específicamente para transferir datos entre las capas de la aplicación (por ejemplo, entre el frontend y el backend).
        //os DTOs pueden evolucionar independientemente de la entidad de dominio. 
        //Por ejemplo, es posible que necesites agregar campos adicionales en un DTO para satisfacer nuevos requisitos de la interfaz de usuario sin afectar la estructura de la entidad de domini
        public required string UserName { get; set; }    
        
    }
}