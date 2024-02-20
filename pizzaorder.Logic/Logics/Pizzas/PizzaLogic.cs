using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Data.Services.Pizzas;
using pizzaorder.Logic.DTOs.Pizza;
using PizzaOrderAPI.Data.Repositories.PizzaIngredients;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Logic.Logics.Pizzas
{
    public class PizzaLogic : IPizzaLogic
    {
        private readonly IPizzaIngredientRepository _repository;
        private readonly IPizzaService _pizzaService;

        public PizzaLogic(IPizzaIngredientRepository repository, IPizzaService pizzaService)
        {
            _repository = repository;
            _pizzaService = pizzaService;
        }
        public Response<PizzaDetailDto> CreatePizza(PizzaDto pizzaDto)
        {
            Response<PizzaDetailDto> pizzaDetailResponse = _pizzaService.CreatePizza(pizzaDto);
            return pizzaDetailResponse;
        }
    }
}
