using AutoMapper;

namespace PizzaOrderAPI.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IMapper _mapper;

        public LoginService(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
