using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pizzaorder.Data.DTOs.Pagination;
using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Data.Resources.Messages;
using pizzaorder.Data.Services.Image;
using pizzaorder.Data.Services.Pagination;
using pizzaorder.Logic.DTOs.Pizza;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Data.Repositories.Ingredients;
using PizzaOrderAPI.Data.Repositories.PizzaIngredients;
using PizzaOrderAPI.Data.Repositories.Pizzas;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.Services.Pizzas
{
    public class PizzaService : IPizzaService
    {
       
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IPizzaIngredientRepository _pizzaIngredientRepository;

        public PizzaService(IPizzaRepository pizzaRepository, IMapper mapper, IImageService imageService, IIngredientRepository ingredientRepository, IPizzaIngredientRepository pizzaIngredientRepository)
        {
            _pizzaRepository = pizzaRepository;
            _mapper = mapper;
            _imageService = imageService;
            _ingredientRepository = ingredientRepository;
            _pizzaIngredientRepository = pizzaIngredientRepository;
        }
        /// <summary>
        /// Creates a Pizza object from a PizzaDto object.
        /// </summary>
        /// <param name="pizzaDto">The PizzaDto object containing pizza data.</param>
        /// <returns>A Pizza object populated with data from the PizzaDto object.</returns>
        private Pizza CreatePizzaFromDto(PizzaDto pizzaDto)
        {
            // Create a new Pizza object
            Pizza pizza = new Pizza()
            {
                Name = pizzaDto.Name
            };

            // Convert the image of the pizza from the PizzaDto object to a string 
            pizza.Image = _imageService.ConvertImageToString(pizzaDto.Image);

            // Return the populated Pizza object
            return pizza;
        }
        /// <summary>
        /// Adds a pizza to the repository and creates a response based on the operation result.
        /// </summary>
        /// <param name="pizza">The Pizza object to be added.</param>
        /// <param name="ingredientIdList">The list of ingredient IDs for the pizza.</param>
        /// <returns>A response containing either success message and pizza details or failure message.</returns>
        private Response<PizzaDetailDto> AddPizzaAndCreateResponse(Pizza pizza, List<int> ingredientIdList)
        {
            // Adds the provided pizza along with its ingredients to the repository.
            Pizza addedPizza = _pizzaRepository.CreatePizza(pizza, ingredientIdList);

            // Checks if the pizza was successfully added to the repository.
            if (addedPizza.Id <= 0)
            {
                // Returns a failure message response if the pizza addition failed.
                return Response<PizzaDetailDto>.CreateFailureMessage(Error.PIZZA_NOT_ADDED_MESSAGE);
            }

            // Maps the added pizza details to a PizzaDetailDto object.
            PizzaDetailDto pizzaDetailDto = _mapper.Map<PizzaDetailDto>(pizza);
            pizzaDetailDto.IngredientIdList = ingredientIdList;

            // Creates a success message response with the pizza details.
            return Response<PizzaDetailDto>.CreateSuccessMessage(pizzaDetailDto, Success.PIZZA_ADDED_SUCCESS_MESSAGE);
        }
        /// <summary>
        /// Creates a new pizza based on the provided PizzaDto object, adds it to the repository, and returns a response.
        /// </summary>
        /// <param name="pizzaDto">The PizzaDto object containing pizza details.</param>
        /// <returns>A response containing either success message and pizza details or failure message.</returns>
        public Response<PizzaDetailDto> CreatePizza(PizzaDto pizzaDto)
        {
            // Check if a pizza with the same name already exists.
            bool isPizzaExist = _pizzaRepository.IsPizzaExist(pizzaDto.Name);
            if (isPizzaExist)
            {
                // Returns a failure message response if the pizza already exists.
                return Response<PizzaDetailDto>.CreateFailureMessage(Error.PIZZA_ALREADY_ADDED_MESSAGE);
            }

            // Creates a new Pizza object from the provided PizzaDto.
            Pizza pizza = CreatePizzaFromDto(pizzaDto);

            // Adds the pizza to the repository and creates a response based on the operation result.
            Response<PizzaDetailDto> addingPizzaResult = AddPizzaAndCreateResponse(pizza, pizzaDto.IngredientIdList);

            return addingPizzaResult;
        }

        //-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Creates an Ingredient object from the provided IngredientDto.
        /// </summary>
        /// <param name="ingredientDto">The data transfer object containing ingredient information.</param>
        /// <returns>An instance of the Ingredient class populated with data from the DTO.</returns>
        private Ingredient CreateIngredientFromDto(IngredientDto ingredientDto)
        {
            // Create a new Ingredient instance and initialize its properties
            Ingredient ingredient = new Ingredient()
            {

                Name = ingredientDto.Name,
                Type = ingredientDto.Type,
                Price = ingredientDto.Price,
            };

            // Convert and set the image of the ingredient using the ImageService
            ingredient.Image = _imageService.ConvertImageToString(ingredientDto.Image);

            // Return the populated Ingredient object
            return ingredient;
        }
        /// <summary>
        /// Adds a new ingredient to the repository and creates a response with detailed information.
        /// </summary>
        /// <param name="ingredient">The ingredient to be added.</param>
        /// <returns>A response containing either success with ingredient details or failure with an error message.</returns>
        private Response<IngredientDetailDto> AddIngredientAndCreateResponse(Ingredient ingredient)
        {
            // Add the ingredient to the repository and get the generated ingredient ID
            int ingredientId = _ingredientRepository.Add(ingredient);

            // Check if the ingredient was not added successfully
            if (ingredientId is -1)
            {
                // Create a failure response with an error message
                return Response<IngredientDetailDto>.CreateFailureMessage(Error.INGREDIENT_NOT_ADDED_MESSAGE);
            }

            // Map the ingredient to IngredientDetailDto
            IngredientDetailDto ingredientDetailDto = _mapper.Map<IngredientDetailDto>(ingredient);
            ingredientDetailDto.Id = ingredientId;

            // Create a success response with the mapped ingredient details and success message
            return Response<IngredientDetailDto>.CreateSuccessMessage(ingredientDetailDto, Success.INGREDIENT_ADDED_SUCCESS_MESSAGE);
        }

        /// <summary>
        /// Adds a new ingredient using the provided IngredientDto and returns the result as a response.
        /// </summary>
        /// <param name="ingredientDto">The data transfer object containing ingredient information.</param>
        /// <returns>A response containing either success with ingredient details or failure with an error message.</returns>
        public Response<IngredientDetailDto> AddIngredient(IngredientDto ingredientDto)
        {
            // Check if the ingredient with the same name already exists
            bool isIngredientExist = _ingredientRepository.IsIngredientExist(ingredientDto.Name);
            if (isIngredientExist)
            {
                return Response<IngredientDetailDto>.CreateFailureMessage(Error.INGREDIENT_ALREADY_ADDED_MESSAGE);
            }

            // Create an Ingredient object from the provided IngredientDto
            Ingredient ingredient = CreateIngredientFromDto(ingredientDto);

            // Call the private method to add the ingredient and create a response
            Response<IngredientDetailDto> addingIngredientResult = AddIngredientAndCreateResponse(ingredient);

            // Return the result of adding the ingredient
            return addingIngredientResult;
        }

        //------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a list of ingredients based on the provided list of ingredient IDs.
        /// </summary>
        /// <param name="ingredientListId">A list of integer IDs representing ingredients.</param>
        /// <returns>
        /// A response containing a list of ingredients if found; otherwise, a failure response indicating that the ingredient list was not found.
        /// </returns>
        public Response<List<Ingredient>> GetIngredientList(List<int> ingredientListId)
        {
            // Define a filter function to filter ingredients based on their IDs.
            Func<Ingredient, bool> filterFunction = p => ingredientListId.Contains(p.Id);

            // Retrieve specific ingredients from the repository based on the filter function.
            List<Ingredient>? specificIngredients = _ingredientRepository.Get(filterFunction);

            // If specific ingredients are not found, return a failure response.
            if (specificIngredients is null)
            {
                return Response<List<Ingredient>>.CreateFailureMessage(Error.INGREDIENT_LIST_NOT_FOUND_MESSAGE);
            }

            // If specific ingredients are found, return a success response with the list of ingredients.
            return Response<List<Ingredient>>.CreateSuccessMessage(specificIngredients, Success.INGREDIENT_LIST_FOUND_MESSAGE);
        }

        //------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves a paginated list of all pizzas.
        /// </summary>
        /// <param name="paginationDto">Pagination data.</param>
        /// <returns>A response containing the paginated list of all pizzas.</returns>
        /// <remarks>This method retrieves a paginated list of all pizzas based on the provided pagination data.
        /// It delegates the retrieval of pizzas to the pizza ingredient repository and returns the response.</remarks>
        public Response<ListPaginationDto<PizzaDetailDto>> GetAllPizza(PaginationDto paginationDto)
        {
            // Retrieve a paginated list of all pizzas
            ListPaginationDto<PizzaDetailDto> pizzaDetailList = _pizzaIngredientRepository.GetAllPizzaWithPagination(paginationDto.Page, paginationDto.PageResult);

            // Check if the pizza list is null
            if (pizzaDetailList is null)
            {
                // If the pizza list is null, return a failure response
                return Response<ListPaginationDto<PizzaDetailDto>>.CreateFailureMessage(Error.PIZZA_LIST_NOT_FOUND_MESSAGE);
            }

            // If the pizza list is not null, return a success response with the pizza list
            return Response<ListPaginationDto<PizzaDetailDto>>.CreateSuccessMessage(pizzaDetailList, Success.PIZZA_LIST_FOUND_MESSAGE);
        }

        /// <summary>
        /// Retrieves a paginated list of pizzas based on the specified pizza name.
        /// </summary>
        /// <param name="pizzaNameDto">Data containing the pizza name and pagination details.</param>
        /// <returns>A response containing the paginated list of pizzas matching the specified pizza name.</returns>
        /// <remarks>This method retrieves a paginated list of pizzas based on the specified pizza name and pagination details.
        /// It delegates the retrieval of pizzas to the pizza repository and returns the response.</remarks>
        public Response<ListPaginationDto<PizzaDetailDto>> GetSpecificPizzaByName(PizzaNameDto pizzaNameDto)
        {
            // Retrieve a paginated list of pizzas based on the specified pizza name
            ListPaginationDto<PizzaDetailDto> pizzaDetailList = _pizzaRepository.GetSpecificPizzaByName(pizzaNameDto.PizzaName, pizzaNameDto.PaginationDto.Page, pizzaNameDto.PaginationDto.PageResult);

            // Check if the pizza list is null
            if (pizzaDetailList is null)
            {
                // If the pizza list is null, return a failure response
                return Response<ListPaginationDto<PizzaDetailDto>>.CreateFailureMessage(Error.PIZZA_LIST_NOT_FOUND_MESSAGE);
            }

            // If the pizza list is not null, return a success response with the pizza list
            return Response<ListPaginationDto<PizzaDetailDto>>.CreateSuccessMessage(pizzaDetailList, Success.PIZZA_LIST_FOUND_MESSAGE);
        }

        /// <summary>
        /// Retrieves details of a specific pizza based on the provided pizza ID.
        /// </summary>
        /// <param name="pizzaIdDto">Data transfer object containing the pizza ID.</param>
        /// <returns>
        /// A response object containing details of the specific pizza.
        /// If the pizza is found, returns a successful response with the pizza details.
        /// If the pizza is not found, returns a failure response with an appropriate error message.
        /// </returns>
        public Response<PizzaDetailDto> GetSpecificPizzaById(PizzaIdDto pizzaIdDto)
        {
            // Retrieve details of the specific pizza based on the provided pizza ID
            PizzaDetailDto pizzaDetailDto = _pizzaRepository.GetSpecificPizzaById(pizzaIdDto.PizzaId);

            // Check if the retrieved pizza details are null
            if (pizzaDetailDto is null)
            {
                // If the pizza details are null, return a failure response with an appropriate error message
                return Response<PizzaDetailDto>.CreateFailureMessage(Error.PIZZA_NOT_FOUND_MESSAGE);
            }

            // If the pizza details are not null, return a success response with the pizza details
            return Response<PizzaDetailDto>.CreateSuccessMessage(pizzaDetailDto, Success.PIZZA_FOUND_MESSAGE);
        }
    }
}
