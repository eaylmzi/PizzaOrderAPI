using PizzaOrderAPI.Data.Repositories.PizzaIngredients;
using PizzaOrderAPI.Data.Repositories.Users;
using PizzaOrderAPI.Logic.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PizzaOrderAPI.Logic.Models.ApiResponses;
using AutoMapper;
using PizzaOrder.Data.Models;
using pizzaorder.Data.Services.Login;
using pizzaorder.Data.Resources.Messages;
using pizzaorder.Data.DTOs.Login;
using System.Xml.Linq;
using pizzaorder.Data.Services.Cipher;

namespace PizzaOrderAPI.Logic.Logics.Users
{
    public class UserLogic : IUserLogic
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly ILoginService _loginService;
        private readonly ICipherService _cipherService;

        public UserLogic(IMapper mapper, IUserRepository repository, ILoginService loginService, ICipherService cipherService)
        {
            _mapper = mapper;
            _repository = repository;
            _loginService = loginService;
            _cipherService = cipherService;
        }
        

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="registrationDto">The data transfer object containing user registration information.</param>
        /// <param name="token">The authentication token associated with the user registration.</param>
        /// <returns>
        /// A response containing the registration details and a message indicating success or failure.
        /// If the email already exists, returns a failure response with an error message.
        /// If the user registration fails, returns a failure response with a registration failed message.
        /// Otherwise, returns a success response with the registration details.
        /// </returns>
        public Response<SignupDetailDto> RegisterUserToSystem(RegistrationDto registrationDto, string token)
        {
            // Call the RegisterUser method in the login service to register an user to system.
            Response<SignupDetailDto> signupDetailResponse = _loginService.RegisterUser(registrationDto, token);
            return signupDetailResponse;
          
        }


        /// <summary>
        /// Method that allows a user to log in.
        /// </summary>
        /// <param name="loginDto">LoginDto object containing login information.</param>
        /// <returns>Response object containing the result of the login process.</returns>
        public Response<SignupDetailDto> Login(LoginDto loginDto)
        {
            // Performs user login .
            Response<SignupDetailDto> loginResult = _loginService.LoginUser(loginDto);

            // Returns the result of the login process.
            return loginResult;
        }



    }
}
