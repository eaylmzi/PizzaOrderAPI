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
        public int Page { get; set; } 
        public float PageResult { get; set; } 
    }
}
