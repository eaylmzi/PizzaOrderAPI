using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Logic.DTOs.Login
{
    public class LoginDto
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
