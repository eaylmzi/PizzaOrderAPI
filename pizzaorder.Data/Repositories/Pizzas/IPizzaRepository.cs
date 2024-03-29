﻿using pizzaorder.Data.DTOs.Pagination;
using pizzaorder.Data.DTOs.Pizza;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Data.Repositories.RepositoriesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Data.Repositories.Pizzas
{
    public interface IPizzaRepository : IRepositoryBase<Pizza>
    {
        public bool IsPizzaExist(string pizzaName);
        public Pizza CreatePizza(Pizza pizza, List<int> ingredientIdList);
        public ListPaginationDto<PizzaDetailDto> GetSpecificPizzaByName(string pizzaName, int page, float pageResult);
        public PizzaDetailDto? GetSpecificPizzaById(int pizzaId);
    }
}
