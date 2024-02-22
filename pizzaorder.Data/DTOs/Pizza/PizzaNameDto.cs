using pizzaorder.Data.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.DTOs.Pizza
{
    public class PizzaNameDto
    {
        public string PizzaName { get; set; } = null!;
        public PaginationDto PaginationDto { get; set; } = null!;
    }
}
