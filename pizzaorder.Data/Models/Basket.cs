using System;
using System.Collections.Generic;

namespace PizzaOrder.Data.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PizzaId { get; set; }
        public string CustomizedPizzaUuid { get; set; } = null!;
        public int? ExtraIngredientId { get; set; }
        public string Status { get; set; } = null!;
    }
}
