using Microsoft.AspNetCore.Mvc;
using pizzaorder.Data.DTOs.Pizza;
using PizzaOrderAPI.Logic.DTOs.Login;
using PizzaOrderAPI.Logic.Logics.Ingredients;
using PizzaOrderAPI.Logic.Logics.Users;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using PizzaOrderAPI.Resources.Jwt;
using PizzaOrderAPI.Services.SecurityServices.Jwt;
using System.Text;

namespace PizzaOrderAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1")]
    public class PizzaController : Controller
    {

        private readonly IUserLogic _userLogic;
        private readonly IJwtService _jwtService;
        private readonly IIngredientLogic _ingredientLogic;
        public PizzaController(IUserLogic userLogic, IJwtService jwtService, IIngredientLogic ingredientLogic)
        {
            _userLogic = userLogic;
            _jwtService = jwtService;
            _ingredientLogic = ingredientLogic;
        }


        /*
        [HttpPost]
        public IActionResult DisplayImage()
        {
                
                return File(_ingredientLogic.GetIngredient(1).Image, "image/jpg"); // veya "image/png" veya "image/gif" gibi uygun MIME türünü belirtebilirsiniz
          
        }
 */
    }
}
