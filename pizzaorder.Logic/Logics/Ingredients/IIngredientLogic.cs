using pizzaorder.Data.DTOs.Pagination;
using pizzaorder.Data.DTOs.Pizza;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Logic.Logics.Ingredients
{
    public interface IIngredientLogic
    {
        public Response<IngredientDetailDto> AddIngredient(IngredientDto ingredientDto);
        public Response<ListPaginationDto<Ingredient>> GetIngredient(IngredientTypeDto ingredientTypeDto);
    }
}
