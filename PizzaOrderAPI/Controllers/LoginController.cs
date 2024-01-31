using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using pizzaorder.Logic.DTOs.Login;
using PizzaOrderAPI.Logic.DTOs.Login;
using PizzaOrderAPI.Logic.Logics.Users;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using PizzaOrderAPI.Resources.Jwt;
using PizzaOrderAPI.Services.SecurityServices.Jwt;

namespace PizzaOrderAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1")]
    public class LoginController : Controller
    {

        private readonly IUserLogic _userLogic;
        private readonly IJwtService _jwtService;
        public LoginController(IUserLogic userLogic, IJwtService jwtService)
        {
            _userLogic = userLogic;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Registers a new user by processing the registration data received in the request body.
        /// </summary>
        /// <param name="registrationDto">The data transfer object containing user registration information.</param>
        /// <returns>An ActionResult containing a response with the registration details and a message indicating success or failure.</returns>
        [HttpPost]
        public ActionResult<Response<SignupDetailDto>> Register([FromBody] RegistrationDto registrationDto)
        {
            try
            {
                // Create a JWT token for the user with the CUSTOMER role
                string userToken = _jwtService.CreateToken(Role.CUSTOMER);

                // Call the RegisterUserToSystem method in the user logic to register the user
                Response<SignupDetailDto> response = _userLogic.RegisterUserToSystem(registrationDto, userToken);

                // Return an Ok result with the registration response
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a BadRequest result with an exception response
                return BadRequest(new Response<Exception> { Message = ex.Message, Progress = false });
            }
        }

        [HttpPost]
        public ActionResult<Response<SignupDetailDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                Response<SignupDetailDto> response = _userLogic.Login(loginDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a BadRequest result with an exception response
                return BadRequest(new Response<Exception> { Message = ex.Message, Progress = false });
            }
        }




    }
}
