using pizzaorder.Data.DTOs.Login;
using PizzaOrderAPI.Logic.DTOs.Login;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Logic.Logics.Users
{
    public interface IUserLogic
    {
        public Response<SignupDetailDto> RegisterUserToSystem(RegistrationDto registrationDto, string token);
        public Response<SignupDetailDto> Login(LoginDto loginDto);
    }
}
