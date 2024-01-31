using AutoMapper;
using PizzaOrderAPI.Logic.DTOs.Login;
using PizzaOrder.Data.Models;

namespace PizzaOrderAPI.Logic.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
           CreateMap<RegistrationDto, User>();
           CreateMap<User, SignupDetailDto>();
            
        }
    }

}
