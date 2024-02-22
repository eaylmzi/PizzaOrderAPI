using pizzaorder.Data.DTOs.Pagination;
using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Logic.DTOs.Pizza;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Logic.Logics.Pizzas
{
    public interface IPizzaLogic
    {
        public Response<PizzaDetailDto> CreatePizza(PizzaDto pizzaDto);
        public Response<ListPaginationDto<PizzaDetailDto>> GetAllPizza(PaginationDto paginationDto);
        public Response<PizzaDetailDto> GetSpecificPizzaById(PizzaIdDto pizzaIdDto);
        public Response<ListPaginationDto<PizzaDetailDto>> GetSpecificPizzaByName(PizzaNameDto pizzaNameDto);
    }
}
