using AutoMapper;
using Microsoft.AspNetCore.Http;
using pizzaorder.Data.DTOs.Pizza;
using pizzaorder.Data.Resources.Messages;
using pizzaorder.Data.Services.Image;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Data.Repositories.Ingredients;
using PizzaOrderAPI.Data.Repositories.Pizzas;
using PizzaOrderAPI.Logic.Models.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.Services.Pizza
{
    public class PizzaService : IPizzaService
    {
       
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public PizzaService(IPizzaRepository pizzaRepository, IMapper mapper, IImageService imageService, IIngredientRepository ingredientRepository)
        {
            _pizzaRepository = pizzaRepository;
            _mapper = mapper;
            _imageService = imageService;
            _ingredientRepository = ingredientRepository;
        }

        public Response<PizzaDetailDto> AddPizza()
        {
            return null;
        }
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
    }
}
