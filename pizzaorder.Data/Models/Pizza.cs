using System;
using System.Collections.Generic;

namespace PizzaOrder.Data.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[] Image { get; set; } = null!;
    }
}
