using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.DTOs.Pizza
{
    public class IngredientDto
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public double Price { get; set; }
        public IFormFile Image { get; set; } = null!;
    }
}
