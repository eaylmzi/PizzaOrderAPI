using pizzaorder.Data.Repositories.PizzaIngredients;
using pizzaorder.Data.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Logic.Logics.Users
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _repository;

        public UserLogic(IUserRepository repository)
        {
            _repository = repository;
        }
    }
}
