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
    public interface IPizzaIngredientRepository : IRepositoryBase<PizzaIngredient>
    {
        public ListPaginationDto<PizzaDetailDto> GetAllPizzaWithPagination(int page, float pageSize);
    }
}
