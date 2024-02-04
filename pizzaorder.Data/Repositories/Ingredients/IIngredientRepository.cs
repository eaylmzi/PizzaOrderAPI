using PizzaOrder.Data.Models;
using PizzaOrderAPI.Data.Repositories.RepositoriesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Data.Repositories.Ingredients
{
    public interface IIngredientRepository : IRepositoryBase<Ingredient>
    {
        public bool IsIngredientExist(string ingredientName);
    }
}
