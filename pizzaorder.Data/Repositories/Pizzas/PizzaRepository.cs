using Microsoft.EntityFrameworkCore;
using pizzaorder.Data.DTOs.Pagination;
using pizzaorder.Data.DTOs.Pizza;
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
        /// <summary>
        /// Checks if a pizza with the specified name exists in the database.
        /// </summary>
        /// <param name="pizzaName">The name of the pizza to check for existence.</param>
        /// <returns>True if a pizza with the specified name exists; otherwise, false.</returns>
        /// <remarks>This method checks if there is a pizza record in the database matching the specified pizza name.</remarks>
        public bool IsPizzaExist(string pizzaName)
        {
            // Checks if there is a record in the Pizza table matching the specified pizza name.
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

        /// <summary>
        /// Retrieves a paginated list of specific Pizza details based on the pizza name.
        /// </summary>
        /// <param name="pizzaName">The name of the Pizza to search for.</param>
        /// <param name="page">The desired page number.</param>
        /// <param name="pageResult">The number of items per page.</param>
        /// <returns>A paginated list of specific Pizza details.</returns>
        /// <remarks>This method retrieves a paginated list of Pizza details based on the provided pizza name, page number, and page size.
        /// It filters the pizzas based on the provided name, orders them by name, and constructs a pagination result containing
        /// the list of pizzas, current page number, page count, and total pizza count.</remarks>
        public ListPaginationDto<PizzaDetailDto> GetSpecificPizzaByName(string pizzaName, int page, float pageResult)
        {
            // Retrieve pizzas matching the provided name
            var results = pizzaQuery
                              .Where(e => e.Name.Length > 3 && e.Name.Contains(pizzaName))
                              .ToList();

            // Count the total number of pizzas
            int totalPizzas = results.Count();

            // Get the desired list of pizzas for the requested page
            var desiredPizzas = pizzaQuery
                .Where(e => e.Name.Length > 3 && e.Name.Contains(pizzaName))
                .OrderBy(p => p.Name)
                .Select(p => new PizzaDetailDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Image = p.Image,
                    IngredientIdList = pizzaIngredientQuery
                        .Where(pi => pi.PizzaId == p.Id)
                        .Select(pi => pi.IngredientId)
                        .ToList()
                })
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();

            // Create a pagination result containing the list of pizzas, current page, page count, and total pizza count
            var paginationResult = new ListPaginationDto<PizzaDetailDto>()
            {
                Items = desiredPizzas,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling((double)totalPizzas / pageResult),
                TotalItem = totalPizzas
            };

            // Return the pagination result
            return paginationResult;
        }

        /// <summary>
        /// Retrieves pizza details for a specific pizza ID.
        /// </summary>
        /// <param name="pizzaId">The ID of the pizza being searched for.</param>
        /// <returns>
        /// A PizzaDetailDto object containing pizza details for a specific pizza ID.
        /// Returns null if the pizza is not found.
        /// </returns>
        public PizzaDetailDto? GetSpecificPizzaById(int pizzaId)
        {
            // Finds the desired pizza with the pizza query
            var desiredPizza = pizzaQuery
                .Where(e => e.Id == pizzaId)
                .Select(p => new PizzaDetailDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Image = p.Image,
                    IngredientIdList = pizzaIngredientQuery
                        .Where(pi => pi.PizzaId == p.Id)
                        .Select(pi => pi.IngredientId)
                        .ToList()
                })
                .SingleOrDefault();

            // Returns the found pizza
            return desiredPizza;
        }

    }
}
