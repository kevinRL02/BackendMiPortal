using AutoMapper;
using MySqlX.XDevAPI.CRUD;
using UserService1.Dtos;
using UserService1.Models;

namespace UserService1.Profiles{
    public class UserProfile : Profile{
        public UserProfile(){
            //Source -> Target
            CreateMap<User,UserReadDto>();
            //Ahora cambia el source y el target
            CreateMap<UserCreateDto,User>();
        }
    }
}