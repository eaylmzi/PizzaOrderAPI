using PizzaOrderAPI.Data.Repositories.PizzaIngredients;
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

        public PizzaLogic(IPizzaIngredientRepository repository)
        {
            _repository = repository;
        }
    }
}
