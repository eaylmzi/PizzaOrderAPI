using pizzaorder.Data.Repositories.Ingredients;
using pizzaorder.Data.Repositories.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Logic.Logics.Orders
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
