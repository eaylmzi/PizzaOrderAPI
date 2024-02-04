using pizzaorder.Data.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.Services.Pagination
{
    public interface IPaginationService<T> where T : class
    {
        public ListPaginationDto<T> GetItemsWithPagination(Func<T, bool> filterFunc, int page, float pageResult);
    }
}
