namespace PizzaOrderAPI.Services.SecurityServices.Jwt
{
    public interface IJwtService
    {
        public string CreateToken(string role);
    }
}
