﻿using PizzaOrderAPI.Data.Repositories.RepositoriesBase;
using PizzaOrder.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PizzaOrderAPI.Data.Repositories.Discounts
{
    public interface IDiscountRepository : IRepositoryBase<Discount>
    {
    }
}
