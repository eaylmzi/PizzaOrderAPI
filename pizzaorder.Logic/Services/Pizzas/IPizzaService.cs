using pizzaorder.Data.DTOs.Pagination;
using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Logic.DTOs.Pizza;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.Services.Pizzas
{
    public interface IPizzaService
    {
        public Response<IngredientDetailDto> AddIngredient(IngredientDto ingredientDto);
        public Response<PizzaDetailDto> CreatePizza(PizzaDto pizzaDto);
        public Response<ListPaginationDto<PizzaDetailDto>> GetAllPizza(PaginationDto paginationDto);
        public Response<List<Ingredient>> GetIngredientList(List<int> ingredientListId);
        public Response<PizzaDetailDto> GetSpecificPizzaById(PizzaIdDto pizzaIdDto);
        public Response<ListPaginationDto<PizzaDetailDto>> GetSpecificPizzaByName(PizzaNameDto pizzaNameDto);
    }
}
