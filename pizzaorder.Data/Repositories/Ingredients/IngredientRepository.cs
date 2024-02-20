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
        private readonly PizzaOrderDBContext _context;
        private DbSet<Ingredient> query { get; set; }
        public IngredientRepository(PizzaOrderDBContext context)
        {
            _context = context;
            query = _context.Set<Ingredient>();
        }
        public bool IsIngredientExist(string ingredientName)
        {
            // Checks if there is a record in the Ingredient table matching the specified ingredient name.
            return query.Any(u => u.Name == ingredientName);
        }


    }
}
