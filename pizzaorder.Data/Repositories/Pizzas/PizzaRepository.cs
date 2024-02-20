using Microsoft.EntityFrameworkCore;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Data.Repositories.RepositoriesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Data.Repositories.Pizzas
{
    public class PizzaRepository : RepositoryBase<Pizza>, IPizzaRepository
    {
        private PizzaOrderDBContext _context { get; set; }
        private DbSet<Pizza> pizzaQuery { get; set; }
        private DbSet<Ingredient> ingredientQuery { get; set; }
        private DbSet<PizzaIngredient> pizzaIngredientQuery { get; set; }
        public PizzaRepository(PizzaOrderDBContext context)
        {
            _context = context;
            pizzaQuery = _context.Set<Pizza>();
            ingredientQuery = _context.Set<Ingredient>();
            pizzaIngredientQuery = _context.Set<PizzaIngredient>();
        }
        public bool IsPizzaExist(string pizzaName)
        {
            // Checks if there is a record in the Ingredient table matching the specified ingredient name.
            return pizzaQuery.Any(u => u.Name == pizzaName);
        }
        /// <summary>
        /// Creates a new pizza along with its ingredients and saves them to the database, rolling back the transaction in case of failure.
        /// </summary>
        /// <param name="pizza">The pizza object to be added.</param>
        /// <param name="ingredientIdList">The list of ingredient IDs for the pizza.</param>
        /// <returns>The newly created pizza object, or an empty pizza object if an error occurs.</returns>
        public Pizza CreatePizza(Pizza pizza, List<int> ingredientIdList)
        {
            // Initiates a transaction for database operations.
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Adds the pizza to the database and retrieves its ID.
                    int pizzaId = Add(pizza);

                    // Prepares pizza-ingredient associations.
                    List<PizzaIngredient> pizzaIngredientList = new List<PizzaIngredient>();
                    foreach (int ingredientId in ingredientIdList)
                    {
                        PizzaIngredient pizzaIngredient = new PizzaIngredient()
                        {
                            IngredientId = ingredientId,
                            PizzaId = pizzaId
                        };
                        pizzaIngredientList.Add(pizzaIngredient);
                    }

                    // Adds pizza-ingredient associations to the database.
                    pizzaIngredientQuery.AddRange(pizzaIngredientList);

                    // Commits the transaction.
                    _context.SaveChanges();
                    transaction.Commit();

                    // Sets the pizza ID and returns the pizza object.
                    pizza.Id = pizzaId;
                    return pizza;
                }
                // Rolls back the transaction and returns an empty pizza object in case of an exception.
                catch (Exception)
                {
                    transaction.Rollback();
                    return new Pizza();
                }
            }
        }

    }
}
