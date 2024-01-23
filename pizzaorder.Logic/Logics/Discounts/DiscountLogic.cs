﻿using pizzaorder.Data.Repositories.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Logic.Logics.Discounts
{
    public class DiscountLogic : IDiscountLogic
    {
        private readonly IDiscountRepository _repository;

        public DiscountLogic(IDiscountRepository repository)
        {
            _repository = repository;
        }
    }
}
