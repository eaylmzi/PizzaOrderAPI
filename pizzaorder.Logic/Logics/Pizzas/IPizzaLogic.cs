using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Logic.DTOs.Pizza;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Logic.Logics.Pizzas
{
    public interface IPizzaLogic
    {
        public Response<PizzaDetailDto> CreatePizza(PizzaDto pizzaDto);
    }
}
