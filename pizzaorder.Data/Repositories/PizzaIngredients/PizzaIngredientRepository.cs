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

namespace PizzaOrderAPI.Data.Repositories.PizzaIngredients
{
    public class PizzaIngredientRepository : RepositoryBase<PizzaIngredient>, IPizzaIngredientRepository
    {
        private PizzaOrderDBContext _context { get; set; }
        private DbSet<PizzaIngredient> pizzaIngredientQuery { get; set; }
        private DbSet<Pizza> pizzaQuery { get; set; }
        public PizzaIngredientRepository(PizzaOrderDBContext context)
        {
            _context = context;
            pizzaIngredientQuery = _context.Set<PizzaIngredient>();
            pizzaQuery =  _context.Set<Pizza>();
        }

        /// <summary>
        /// Retrieves a paginated list of Pizza details.
        /// </summary>
        /// <param name="page">The desired page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of Pizza details.</returns>
        /// <remarks>This method retrieves a paginated list of Pizza details based on the provided page number and page size.
        /// It calculates the total number of pizzas available, retrieves the desired list of pizzas for the requested page,
        /// and constructs a pagination result containing the list of pizzas, current page number, page count, and total pizza count.</remarks>
        public ListPaginationDto<PizzaDetailDto> GetAllPizzaWithPagination(int page, float pageSize)
        {
            // Count the total number of pizzas
            int totalPizzas = pizzaQuery.Count();

            // Get the desired list of pizzas for the requested page
            var desiredPizzas = pizzaQuery
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
                .Skip((page - 1) * (int)pageSize)
                .Take((int)pageSize)
                .ToList();

            // Create a pagination result containing the list of pizzas, current page, page count, and total pizza count
            var paginationResult = new ListPaginationDto<PizzaDetailDto>()
            {
                Items = desiredPizzas,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling((double)totalPizzas / pageSize),
                TotalItem = totalPizzas
            };

            // Return the pagination result
            return paginationResult;
        }
    }
}
