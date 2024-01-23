using PizzaOrder.Data.Models;
using pizzaorder.Data.Repositories.RepositoriesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.Repositories.Ingredients
{
    public class IngredientRepository : RepositoryBase<Ingredient>, IIngredientRepository
    {
    }
}
