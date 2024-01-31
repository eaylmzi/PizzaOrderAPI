using PizzaOrderAPI.Data.Repositories.Ingredients;
using PizzaOrderAPI.Data.Repositories.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Logic.Logics.Orders
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderRepository _repository;

        public OrderLogic(IOrderRepository repository)
        {
            _repository = repository;
        }
    }
}
