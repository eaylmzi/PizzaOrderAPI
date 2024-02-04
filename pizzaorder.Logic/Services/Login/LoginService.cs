using AutoMapper;
using pizzaorder.Data.DTOs.Login;
using pizzaorder.Data.Resources.Messages;
using pizzaorder.Data.Services.Cipher;
using PizzaOrder.Data.Models;
using PizzaOrderAPI.Data.Repositories.Users;
using PizzaOrderAPI.Logic.DTOs.Login;
using PizzaOrderAPI.Logic.Models.ApiResponses;

namespace pizzaorder.Data.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IMapper _mapper;
        private readonly ICipherService _cipherService;
        private readonly IUserRepository _userRepository;

        public LoginService(IMapper mapper, ICipherService cipherService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _cipherService = cipherService;
            _userRepository = userRepository;
        }
        /// <summary>
        /// Checks if the given email address already exists in the system.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>
        /// A response indicating whether the email already exists.
        /// If the email exists, returns a failure response with an error message.
        /// Otherwise, returns a success response with Progress set to true.
        /// </returns>
        private Response<SignupDetailDto> IsEmailExists(string email)
        {
            bool isUserEmailExist = _userRepository.IsEmailExists(email);
            if (isUserEmailExist)
            {
                // If the email already exists, return a failure response with an error message.
                return Response<SignupDetailDto>.CreateFailureMessage(Error.EMAIL_REGISTRATION_FAILED_MESSAGE);
            }
            // If the email does not exist, return a success response with Progress set to true.
            return new Response<SignupDetailDto>() { Progress = true };
        }

        /// <summary>
        /// Maps a User object to a SignupDetailDto and creates a response.
        /// </summary>
        /// <param name="user">The user object to be mapped.</param>
        /// <returns>
        /// A response containing the mapped SignupDetailDto and a message indicating success.
        /// </returns>
        private Response<SignupDetailDto> MapUserToSignupCredentialAndCreateResponse(User user)
        {
            // Map the User object to a SignupDetailDto object using AutoMapper
            SignupDetailDto signupDetail = _mapper.Map<SignupDetailDto>(user);

            // Return a success response with the mapped SignupDetailDto and a success message.
            return Response<SignupDetailDto>.CreateSuccessMessage(signupDetail, Success.LOGIN_SUCCESS_MESSAGE);
        }

        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <param name="user">The user object to be added.</param>
        /// <returns>
        /// A response containing the registration details and a message indicating success or failure.
        /// If adding the user fails, returns a failure response with a registration failed message.
        /// Otherwise, returns a success response with the registration details.
        /// </returns>
        private Response<SignupDetailDto> AddUser(User user)
        {
            // Add the user to the repository and get the added user's ID
            int addedUserId = _userRepository.Add(user);

            // Check if the user was added successfully
            if (addedUserId is -1)
            {
                // If adding the user failed, return a failure response with a registration failed message.
                return Response<SignupDetailDto>.CreateFailureMessage(Error.REGISTRATION_FAILED_MESSAGE);
            }

            // Map the User object to a SignupDetailDto and create a response
            Response<SignupDetailDto> signupDetail = MapUserToSignupCredentialAndCreateResponse(user);

            // Return a success response with the registration details.
            return signupDetail;
        }

        /// <summary>
        /// Generates a password hash and salt for a given password using a cryptographic service.
        /// </summary>
        /// <param name="password">The plain-text password to be hashed.</param>
        /// <param name="passwordHash">The output parameter to store the generated password hash.</param>
        /// <param name="passwordSalt">The output parameter to store the generated password salt.</param>
        private void CreatePasswordHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // Call the _cipherService to create a password hash and salt
            _cipherService.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        }

        /// <summary>
        /// Creates a User object with encrypted password information from the provided RegistrationDto.
        /// </summary>
        /// <param name="registrationDto">The data transfer object containing user registration information.</param>
        /// <param name="token">The authentication token associated with the user registration.</param>
        /// <returns>A User object with encrypted password information.</returns>
        private User CreateUserFromRegistrationDto(RegistrationDto registrationDto, string token)
        {
            // Map RegistrationDto to User using AutoMapper
            User user = _mapper.Map<User>(registrationDto);

            // Call CreatePasswordHashAndSalt to generate password hash and salt
            CreatePasswordHashAndSalt(registrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // Assign token, the generated password hash, and salt to the User object
            user.Passwordhash = passwordHash;
            user.Passwordsalt = passwordSalt;
            user.Token = token;

            return user;
        }

        /// <summary>
        /// Registers a new user by creating a User object with token and encrypted password information.
        /// </summary>
        /// <param name="registrationDto">The data transfer object containing user registration information.</param>
        /// <param name="token">The authentication token associated with the user registration.</param>
        /// <returns>A response containing the signup details and a message indicating success or failure.</returns>
        public Response<SignupDetailDto> RegisterUser(RegistrationDto registrationDto, string token)
        {
            // Check if the provided email already exists in the system.
            Response<SignupDetailDto> emailCheckResult = IsEmailExists(registrationDto.Email);
            if (emailCheckResult.Progress is false)
            {
                // Return a failure response if the email already exists.
                return emailCheckResult;
            }

            // Create a User object with encrypted password information.
            User user = CreateUserFromRegistrationDto(registrationDto, token);

            // Attempt to add the user to the system.
            Response<SignupDetailDto> addingUserResult = AddUser(user);
            return addingUserResult;
        }

        /// <summary>
        /// Checks if the provided password matches the hashed password of the user.
        /// If the password matches, creates a success response with the user's signup details.
        /// If the password does not match, creates a failure response with an error message.
        /// </summary>
        /// <param name="user">The user whose password needs to be verified.</param>
        /// <param name="password">The password to be verified.</param>
        /// <returns>
        /// A response containing the signup details and a message indicating success or failure.
        /// If the password matches, returns a success response with the signup details.
        /// If the password does not match, returns a failure response with an error message.
        /// </returns>
        private Response<SignupDetailDto> IsPasswordMatched(User user, string password)
        {
            // Verify if the provided password matches the hashed password of the user.
            bool isMatched = _cipherService.VerifyPasswordHash(user.Passwordhash, user.Passwordsalt, password);

            if (isMatched)
            {
                // If the password matches, create a success response with the user's signup details.
                Response<SignupDetailDto> signupDetailResponse = MapUserToSignupCredentialAndCreateResponse(user);
                return signupDetailResponse;
            }

            // If the password does not match, create a failure response with an error message.
            return Response<SignupDetailDto>.CreateFailureMessage(Error.CREDENTIALS_NOT_MATCHED_MESSAGE);
        }
        /// <summary>
        /// Logs in a user by verifying the provided login credentials.
        /// </summary>
        /// <param name="loginDto">The data transfer object containing user login information.</param>
        /// <returns>
        /// A response containing the signup details and a message indicating success or failure.
        /// If the user is found and the password matches, returns a success response with the signup details.
        /// If the user is not found, returns a failure response with an error message.
        /// If the password does not match, returns a failure response with an error message.
        /// </returns>
        public Response<SignupDetailDto> LoginUser(LoginDto loginDto)
        {
            // Retrieve the user from the repository based on the provided email.
            User? user = _userRepository.GetSingleByMethod(filter => filter.Email == loginDto.Email);

            if (user is null)
            {
                // If the user is not found, return a failure response with an error message.
                return Response<SignupDetailDto>.CreateFailureMessage(Error.CREDENTIALS_NOT_MATCHED_MESSAGE);
            }

            // Check if the provided password matches the user's password and return the result.
            Response<SignupDetailDto> loginResult = IsPasswordMatched(user, loginDto.Password);
            return loginResult;
        }
    }
}
