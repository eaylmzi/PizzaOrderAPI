using Microsoft.EntityFrameworkCore;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Data.Repositories.RepositoriesBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PizzaOrderAPI.Data.Repositories.Ingredients
{
    public class IngredientRepository : RepositoryBase<Ingredient>, IIngredientRepository 
    {
        PizzaOrderDBContext _context = new PizzaOrderDBContext();
        private DbSet<Ingredient> query { get; set; }
        public IngredientRepository()
        {
            query = _context.Set<Ingredient>();
        }
        public bool IsIngredientExist(string ingredientName)
        {
            // Checks if there is a record in the Users table matching the specified email address.
            return query.Any(u => u.Name == ingredientName);
        }


    }
}
