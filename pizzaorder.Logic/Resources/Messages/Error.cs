namespace pizzaorder.Data.Resources.Messages
{
    public static class Error
    {
        public const string REGISTRATION_FAILED_MESSAGE = "User registration failed.";
        public const string EMAIL_REGISTRATION_FAILED_MESSAGE = "The provided email address is already registered.";
        public const string USER_NOT_FOUND_MESSAGE = "User not found.";
        public const string CREDENTIALS_NOT_MATCHED_MESSAGE = "The credentials are not matched.";


        public const string LIST_NOT_FOUND_MESSAGE = "List is empty.";
        //Ingredient
        public const string INGREDIENT_NOT_ADDED_MESSAGE = "Ingredient could not be added.";
        public const string INGREDIENT_ALREADY_ADDED_MESSAGE = "This ingredient has already been added.";
        public const string INGREDIENT_LIST_NOT_FOUND_MESSAGE = "Ingredient list is empty.";

        //Pizza
        public const string PIZZA_ALREADY_ADDED_MESSAGE = "This pizza has already been added.";
        public const string PIZZA_NOT_ADDED_MESSAGE = "Pizza could not be added.";
        public const string PIZZA_LIST_NOT_FOUND_MESSAGE = "Pizza list is empty.";
        public const string PIZZA_NOT_FOUND_MESSAGE = "Pizza is not found.";

    }

}

