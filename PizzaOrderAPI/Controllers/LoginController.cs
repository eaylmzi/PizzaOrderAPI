using Microsoft.AspNetCore.Mvc;
using pizzaorder.Logic.Logics.Baskets;
using pizzaorder.Logic.Logics.Users;
using PizzaOrderAPI.Services.LoginServices;

namespace PizzaOrderAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1")]
    public class LoginController : Controller
    {

        private readonly IUserLogic _userLogic;
        public LoginController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        public IActionResult a()
        {
            return Ok(5);
        }
    }
}
