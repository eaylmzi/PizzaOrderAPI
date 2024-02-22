using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Data.Services.Pizzas;

using PizzaOrderAPI.Data.Repositories.Baskets;
using PizzaOrderAPI.Data.Repositories.Ingredients;
using PizzaOrderAPI.Logic.DTOs.Login;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PizzaOrder.Data.Models;
using pizzaorder.Data.Resources.Messages;
using pizzaorder.Data.Services.Pagination;
using pizzaorder.Data.DTOs.Pagination;

namespace PizzaOrderAPI.Logic.Logics.Ingredients
{
    public class IngredientLogic : IIngredientLogic
    {
        private readonly IIngredientRepository _repository;
        private readonly IPizzaService _pizzaService;
        private readonly IPaginationService<Ingredient> _paginationService;
        public IngredientLogic(IIngredientRepository repository, IPizzaService pizzaService, IPaginationService<Ingredient> paginationService)
        {
            _repository = repository;
            _pizzaService = pizzaService;
            _paginationService = paginationService;
        }

        /// <summary>
        /// Adds a new ingredient using the provided IngredientDto and returns the result as a response.
        /// </summary>
        /// <param name="ingredientDto">The data transfer object containing ingredient information.</param>
        /// <returns>A response containing either success with ingredient details or failure with an error message.</returns>
        public Response<IngredientDetailDto> AddIngredient(IngredientDto ingredientDto)
        {
            Response<IngredientDetailDto> ingredientDetailResponse = _pizzaService.AddIngredient(ingredientDto);
            return ingredientDetailResponse;
        }

        /// <summary>
        /// Retrieves a paginated and filtered list of ingredients based on the provided parameters.
        /// </summary>
        /// <param name="ingredientTypeDto">Ingredient type parameters.</param>
        /// <returns>A response containing the paginated and filtered list of ingredients.</returns>
        public Response<ListPaginationDto<Ingredient>> GetAllIngredient(IngredientTypeDto ingredientTypeDto)
        {
            // Define the filter function based on ingredient type
            Func<Ingredient, bool> filterFunc = i => i.Type == ingredientTypeDto.Type;

            // Retrieve the ingredient list with pagination
            List<Ingredient>? ingredientList = _repository.GetWithPagination(filterFunc, ingredientTypeDto.PaginationDto.Page, ingredientTypeDto.PaginationDto.PageResult);

            // Check if the ingredient list is null
            if (ingredientList is null)
            {
                return Response<ListPaginationDto<Ingredient>>.CreateFailureMessage(Error.LIST_NOT_FOUND_MESSAGE);
            }

            // Retrieve the paginated and filtered list of ingredients using pagination service
            ListPaginationDto<Ingredient> paginationResult = _paginationService.GetItemsWithPagination(filterFunc, ingredientTypeDto.PaginationDto.Page, ingredientTypeDto.PaginationDto.PageResult);

            // Check if the retrieved list is empty
            bool isListEmpty = paginationResult.Items.Count == 0;
            if (isListEmpty)
            {
                return Response<ListPaginationDto<Ingredient>>.CreateFailureMessage(Error.LIST_NOT_FOUND_MESSAGE);
            }

            // Return a success response with the paginated and filtered list of ingredients
            return Response<ListPaginationDto<Ingredient>>.CreateSuccessMessage(paginationResult, Success.LIST_FOUND_MESSAGE);
        }

        /// <summary>
        /// Retrieves a list of ingredients based on the provided list of ingredient IDs.
        /// </summary>
        /// <param name="ingredientIdList">A list of integer IDs representing ingredients.</param>
        /// <returns>A response containing a list of ingredients.</returns>
        public Response<List<Ingredient>> GetIngredientList(List<int> ingredientIdList)
        {
            // Call the pizza service to get the list of ingredients based on the provided IDs.
            Response<List<Ingredient>> response = _pizzaService.GetIngredientList(ingredientIdList);
            return response;
        }

    }
}
