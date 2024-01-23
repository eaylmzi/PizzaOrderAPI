using System;
using System.Collections.Generic;

namespace PizzaOrder.Data.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string CustomizedPizza { get; set; } = null!;
        public double TotalPrice { get; set; }
        public byte[] OrderedAt { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
