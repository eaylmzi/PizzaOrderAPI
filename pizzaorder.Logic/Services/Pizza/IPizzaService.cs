using pizzaorder.Data.DTOs.Pizza;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.Services.Pizza
{
    public interface IPizzaService
    {
        public Response<IngredientDetailDto> AddIngredient(IngredientDto ingredientDto);
    }
}
