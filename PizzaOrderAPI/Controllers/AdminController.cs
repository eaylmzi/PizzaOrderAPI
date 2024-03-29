﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pizzaorder.Data.DTOs.Pagination;
using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Logic.DTOs.Pizza;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Logic.Logics.Ingredients;
using PizzaOrderAPI.Logic.Logics.Pizzas;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using PizzaOrderAPI.Resources.Jwt;
using System.Collections.Generic;

namespace PizzaOrderAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdminController : Controller
    {
        private readonly IIngredientLogic _ingredientLogic;
        private readonly IPizzaLogic _pizzaLogic;

        public AdminController(IIngredientLogic ingredientLogic, IPizzaLogic pizzaLogic)
        {
            _ingredientLogic = ingredientLogic;
            _pizzaLogic = pizzaLogic;
        }
        [HttpPost]
        public ActionResult<Response<PizzaDetailDto>> CreatePizza([FromForm] PizzaDto pizzaDto)
        {
            try
            {
                Response<PizzaDetailDto> response = _pizzaLogic.CreatePizza(pizzaDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return a BadRequest result with an exception response
                return BadRequest(new Response<Exception> { Message = ex.Message, Progress = false });
            }
        }

        [HttpPost, Authorize(Roles = $"{Role.MANAGER}")]
        public ActionResult<Response<IngredientDetailDto>> AddIngredient([FromForm] IngredientDto ingredientDto)
        {
            try
            {
                Response<IngredientDetailDto> response = _ingredientLogic.AddIngredient(ingredientDto);
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
