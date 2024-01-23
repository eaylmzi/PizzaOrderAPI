using pizzaorder.Data.Repositories.Baskets;
using pizzaorder.Data.Repositories.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Logic.Logics.Ingredients
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
