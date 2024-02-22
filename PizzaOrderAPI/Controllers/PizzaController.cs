using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pizzaorder.Data.DTOs.Pagination;
using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Logic.DTOs.Pizza;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Logic.DTOs.Login;
using PizzaOrderAPI.Logic.Logics.Ingredients;
using PizzaOrderAPI.Logic.Logics.Pizzas;
using PizzaOrderAPI.Logic.Logics.Users;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using PizzaOrderAPI.Resources.Jwt;
using PizzaOrderAPI.Services.SecurityServices.Jwt;
using SQLitePCL;
using System.Collections.Generic;
using System.Data;
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
        private readonly IPizzaLogic _pizzaLogic;
        public PizzaController(IUserLogic userLogic, IJwtService jwtService, IIngredientLogic ingredientLogic, IPizzaLogic pizzaLogic)
        {
            _userLogic = userLogic;
            _jwtService = jwtService;
            _ingredientLogic = ingredientLogic;
            _pizzaLogic = pizzaLogic;
        }

        [HttpPost]
        public ActionResult<Response<List<Ingredient>>> GetIngredientList([FromBody] List<int> ingredientIdList)
        {
            try
            {
                Response<List<Ingredient>> response = _ingredientLogic.GetIngredientList(ingredientIdList);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a BadRequest result with an exception response
                return BadRequest(new Response<Exception> { Message = ex.Message, Progress = false });
            }
        }
        [HttpPost]
        public ActionResult<Response<ListPaginationDto<Ingredient>>> GetAllIngredient([FromBody] IngredientTypeDto ingredientTypeDto)
        {
            try
            {
                Response<ListPaginationDto<Ingredient>> response = _ingredientLogic.GetAllIngredient(ingredientTypeDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a BadRequest result with an exception response
                return BadRequest(new Response<Exception> { Message = ex.Message, Progress = false });
            }
        }
        [HttpPost]
        public ActionResult<Response<ListPaginationDto<PizzaDetailDto>>> GetAllPizza(PaginationDto paginationDto)
        {
            try
            {
                Response<ListPaginationDto<PizzaDetailDto>> response = _pizzaLogic.GetAllPizza(paginationDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a BadRequest result with an exception response
                return BadRequest(new Response<Exception> { Message = ex.Message, Progress = false });
            }
        }

        [HttpPost]
        public ActionResult<Response<ListPaginationDto<PizzaDetailDto>>> GetSpecificPizzaByName(PizzaNameDto pizzaNameDto)
        {
            try
            {
                Response<ListPaginationDto<PizzaDetailDto>> response = _pizzaLogic.GetSpecificPizzaByName(pizzaNameDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a BadRequest result with an exception response
                return BadRequest(new Response<Exception> { Message = ex.Message, Progress = false });
            }
        }
        [HttpPost]
        public ActionResult<Response<PizzaDetailDto>> GetSpecificPizzaById(PizzaIdDto pizzaIdDto)
        {
            try
            {
                Response<PizzaDetailDto> response = _pizzaLogic.GetSpecificPizzaById(pizzaIdDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a BadRequest result with an exception response
                return BadRequest(new Response<Exception> { Message = ex.Message, Progress = false });
            }
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
