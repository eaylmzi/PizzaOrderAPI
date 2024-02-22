using PizzaOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.DTOs.Pagination
{
    public class ListPaginationDto<T>
    {
        public List<T> Items { get; set; } = null!;
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int TotalItem { get; set; }
    }
}
