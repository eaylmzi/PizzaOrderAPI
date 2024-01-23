using System;
using System.Collections.Generic;

namespace PizzaOrder.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public double Price { get; set; }
        public byte[] Image { get; set; } = null!;
    }
}
