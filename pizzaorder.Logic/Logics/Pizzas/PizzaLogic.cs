using pizzaorder.Data.DTOs.Pagination;
using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Data.Services.Pizzas;
using pizzaorder.Logic.DTOs.Pizza;
using PizzaOrderAPI.Data.Repositories.PizzaIngredients;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrderAPI.Logic.Logics.Pizzas
{
    public class PizzaLogic : IPizzaLogic
    {
        private readonly IPizzaIngredientRepository _repository;
        private readonly IPizzaService _pizzaService;

        public PizzaLogic(IPizzaIngredientRepository repository, IPizzaService pizzaService)
        {
            _repository = repository;
            _pizzaService = pizzaService;
        }
        /// <summary>
        /// Creates a new pizza based on the provided pizza data.
        /// </summary>
        /// <param name="pizzaDto">The data of the pizza to be created.</param>
        /// <returns>A response containing the details of the created pizza.</returns>
        /// <remarks>This method delegates the creation of a new pizza to the pizza service and returns the response.</remarks>
        public Response<PizzaDetailDto> CreatePizza(PizzaDto pizzaDto)
        {
            // Delegate the creation of the pizza to the pizza service
            Response<PizzaDetailDto> pizzaDetailResponse = _pizzaService.CreatePizza(pizzaDto);
            return pizzaDetailResponse;
        }

        /// <summary>
        /// Retrieves a paginated list of all pizzas.
        /// </summary>
        /// <param name="paginationDto">Pagination data.</param>
        /// <returns>A response containing the paginated list of all pizzas.</returns>
        /// <remarks>This method delegates the retrieval of all pizzas to the pizza service and returns the response.</remarks>
        public Response<ListPaginationDto<PizzaDetailDto>> GetAllPizza(PaginationDto paginationDto)
        {
            // Delegate the retrieval of all pizzas to the pizza service
            Response<ListPaginationDto<PizzaDetailDto>> pizzaDetailListResponse = _pizzaService.GetAllPizza(paginationDto);
            return pizzaDetailListResponse;
        }

        /// <summary>
        /// Retrieves a paginated list of pizzas based on the specified pizza name.
        /// </summary>
        /// <param name="pizzaNameDto">Data containing the pizza name and pagination details.</param>
        /// <returns>A response containing the paginated list of pizzas matching the specified pizza name.</returns>
        /// <remarks>This method delegates the retrieval of pizzas based on the specified name to the pizza service and returns the response.</remarks>
        public Response<ListPaginationDto<PizzaDetailDto>> GetSpecificPizzaByName(PizzaNameDto pizzaNameDto)
        {
            // Delegate the retrieval of pizzas based on the specified name to the pizza service
            Response<ListPaginationDto<PizzaDetailDto>> pizzaDetailResponse = _pizzaService.GetSpecificPizzaByName(pizzaNameDto);
            return pizzaDetailResponse;
        }
        /// <summary>
        /// Service method to retrieve pizza details based on a specific pizza ID.
        /// </summary>
        /// <param name="pizzaIdDto">Data transfer object carrying the pizza ID.</param>
        /// <returns>
        /// A response object containing pizza details.
        /// If the response object contains successfully retrieved pizza details, it returns a successful response.
        /// If the desired pizza cannot be found or an error occurs, it returns an unsuccessful response containing an appropriate error message.
        /// </returns>
        public Response<PizzaDetailDto> GetSpecificPizzaById(PizzaIdDto pizzaIdDto)
        {
            // Delegate the retrieval of pizzas based on the specified ID to the pizza service
            Response<PizzaDetailDto> pizzaDetailResponse = _pizzaService.GetSpecificPizzaById(pizzaIdDto);
            return pizzaDetailResponse;
        }
    }
}
