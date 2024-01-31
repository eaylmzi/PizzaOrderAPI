using pizzaorder.Logic.DTOs.Login;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Logic.DTOs.Login;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Logic.Services.Login
{
    public interface ILoginService
    {
        public Response<SignupDetailDto> RegisterUser(RegistrationDto registrationDto, string token);
        public Response<SignupDetailDto> LoginUser(LoginDto loginDto);
    }
}
