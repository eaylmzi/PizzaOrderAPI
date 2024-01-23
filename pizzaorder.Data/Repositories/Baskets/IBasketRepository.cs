using pizzaorder.Data.Repositories.RepositoriesBase;
using PizzaOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace pizzaorder.Data.Repositories.Baskets
{
    public interface IBasketRepository : IRepositoryBase<Basket>
    {
    }
}
