using pizzaorder.Data.Repositories.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Logic.Logics.Baskets
{
    public class BasketLogic : IBasketLogic
    {
        private readonly IBasketRepository _repository;

        public BasketLogic(IBasketRepository repository)
        {
            _repository = repository;
        }
    }
}
