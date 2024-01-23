using System;
using System.Collections.Generic;

namespace PizzaOrder.Data.Models
{
    public class PizzaIngredient
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int IngredientId { get; set; }
    }
}
