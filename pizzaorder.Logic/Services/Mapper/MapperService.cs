using AutoMapper;
using PizzaOrderAPI.Logic.DTOs.Login;
using PizzaOrder.Data.Models;
using pizzaorder.Data.DTOs.Pizza;

namespace PizzaOrderAPI.Logic.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
           CreateMap<RegistrationDto, User>();
           CreateMap<User, SignupDetailDto>();

           CreateMap<IngredientDto, Ingredient>();
           CreateMap<Ingredient, IngredientDetailDto>();
        }
    }

}
