using PizzaOrderAPI.Data.Repositories.Orders;
using PizzaOrderAPI.Data.Repositories.PizzaIngredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Logic.Logics.PizzaIngredients
{
    public class PizzaIngredientLogic : IPizzaIngredientLogic
    {
        private readonly IPizzaIngredientRepository _repository;

        public PizzaIngredientLogic(IPizzaIngredientRepository repository)
        {
            _repository = repository;
        }
    }
}
