using System;
using System.Collections.Generic;

namespace PizzaOrder.Data.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public double DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
