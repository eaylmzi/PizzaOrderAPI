namespace PizzaOrderAPI.Services.SecurityServices.Jwt.Models
{
    public class JwtConfig
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;

    }
}
