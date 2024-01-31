using Microsoft.EntityFrameworkCore;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Data.Repositories.RepositoriesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Data.Repositories.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        PizzaOrderDBContext _context = new PizzaOrderDBContext();
        private DbSet<User> query { get; set; }
        public UserRepository()
        {
            query = _context.Set<User>();
        }

        /// <summary>
        /// Checks whether the specified email address exists in the database.
        /// </summary>
        /// <param name="email">The email address to check</param>
        /// <returns>
        /// Returns true if the email address exists in the database, 
        /// otherwise returns false.
        /// </returns>
        public bool IsEmailExists(string email)
        {
            // Checks if there is a record in the Users table matching the specified email address.
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
