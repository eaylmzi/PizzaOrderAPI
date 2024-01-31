using PizzaOrderAPI.Data.Repositories.Baskets;
using PizzaOrderAPI.Data.Repositories.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Logic.Logics.Ingredients
{
    public class IngredientLogic : IIngredientLogic
    {
        private readonly IIngredientRepository _repository;

        public IngredientLogic(IIngredientRepository repository)
        {
            _repository = repository;
        }
    }
}
