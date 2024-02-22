using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.DTOs.Pizza
{
    public class PizzaDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[] Image { get; set; } = null!;
        public List<int> IngredientIdList { get; set; } = null!;
    }
}
