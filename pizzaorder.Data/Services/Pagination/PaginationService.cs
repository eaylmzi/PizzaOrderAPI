using Microsoft.EntityFrameworkCore;
using pizzaorder.Data.DTOs.Pagination;
using PizzaOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.Services.Pagination
{
    public class PaginationService<T> : IPaginationService<T> where T : class
    {
        PizzaOrderDBContext _context = new PizzaOrderDBContext();

        private DbSet<T> query { get; set; }
        public PaginationService()
        {
            query = _context.Set<T>();
        }

        /// <summary>
        /// Retrieves a paginated list of items.
        /// </summary>
        /// <typeparam name="T">Type of items in the list.</typeparam>
        /// <param name="filterFunc">Filter function to apply on the items.</param>
        /// <param name="page">Page number to retrieve.</param>
        /// <param name="pageResult">Number of items per page.</param>
        /// <returns>A pagination result containing the paginated list.</returns>
        public ListPaginationDto<T> GetItemsWithPagination(Func<T, bool> filterFunc, int page, float pageResult)
        {
            // Count the total number of items after applying the filter
            int totalItem = query
                              .Where(filterFunc)
                              .ToList()
                              .Count();

            // Get the desired list of items for the requested page
            var desiredList = query
                              .Where(filterFunc)
                              .Skip((page - 1) * (int)pageResult)
                              .Take((int)pageResult)
                              .ToList();

            // Check if the requested page is non-empty
            bool isNonEmptyPage = (pageResult * page) >= totalItem && (pageResult * page) - pageResult < totalItem;
            if (isNonEmptyPage)
            {
                // Create a pagination result containing the list of items, current page, page count, and total item count
                ListPaginationDto<T> paginationResult = new ListPaginationDto<T>()
                {
                    ListOfSomething = desiredList,
                    CurrentPage = page,
                    PageCount = (int)Math.Ceiling((totalItem / pageResult)),
                    TotalItem = totalItem
                };

                // Return the pagination result
                return paginationResult;
            }

            // Return an empty pagination result if the page is empty
            return new ListPaginationDto<T>();
        }

    }
}
