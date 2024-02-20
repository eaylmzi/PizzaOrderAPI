using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Logic.DTOs.Pizza
{
    public class PizzaDto
    {
        public string Name { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public List<int> IngredientIdList { get; set; } = null!;
    }
}
