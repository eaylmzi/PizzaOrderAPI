using pizzaorder.Data.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.DTOs.Pizza
{
    public class IngredientTypeDto
    {
        public string Type { get; set; } = null!;
        public PaginationDto PaginationDto { get; set; } = null!;
    }
}
