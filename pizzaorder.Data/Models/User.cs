using System;
using System.Collections.Generic;

namespace PizzaOrder.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public byte[] Passwordhash { get; set; } = null!;
        public byte[] Passwordsalt { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
