using PizzaOrderAPI.Data.Repositories.RepositoriesBase;
using PizzaOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Data.Repositories.Baskets
{
    public class BasketRepository : RepositoryBase<Basket> , IBasketRepository 
    {
    }
}
