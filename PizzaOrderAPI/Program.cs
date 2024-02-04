

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pizzaorder.Data.Services.Cipher;
using pizzaorder.Data.Services.Image;
using pizzaorder.Data.Services.Login;
using pizzaorder.Data.Services.Pagination;
using pizzaorder.Data.Services.Pizza;
using PizzaOrderAPI.Data.Repositories.Baskets;
using PizzaOrderAPI.Data.Repositories.Discounts;
using PizzaOrderAPI.Data.Repositories.Ingredients;
using PizzaOrderAPI.Data.Repositories.Orders;
using PizzaOrderAPI.Data.Repositories.PizzaIngredients;
using PizzaOrderAPI.Data.Repositories.Pizzas;
using PizzaOrderAPI.Data.Repositories.Users;
using PizzaOrderAPI.Logic.Logics.Baskets;
using PizzaOrderAPI.Logic.Logics.Discounts;
using PizzaOrderAPI.Logic.Logics.Ingredients;
using PizzaOrderAPI.Logic.Logics.Orders;
using PizzaOrderAPI.Logic.Logics.PizzaIngredients;
using PizzaOrderAPI.Logic.Logics.Pizzas;
using PizzaOrderAPI.Logic.Logics.Users;
using PizzaOrderAPI.Logic.Services.Mapper;
using PizzaOrderAPI.Models.Jwt;
using PizzaOrderAPI.Services.SecurityServices.Jwt;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//AutoMapper
builder.Services.AddAutoMapper(typeof(MapperService).Assembly);

// Data Access Layer Dependency Injection
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPizzaIngredientRepository, PizzaIngredientRepository>();
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Logic Layer Dependency Injection
builder.Services.AddScoped<IBasketLogic, BasketLogic>();
builder.Services.AddScoped<IDiscountLogic, DiscountLogic>();
builder.Services.AddScoped<IIngredientLogic, IngredientLogic>();
builder.Services.AddScoped<IOrderLogic, OrderLogic>();
builder.Services.AddScoped<IPizzaIngredientLogic, PizzaIngredientLogic>();
builder.Services.AddScoped<IPizzaLogic, PizzaLogic>();
builder.Services.AddScoped<IUserLogic, UserLogic>();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped(typeof(IPaginationService<>), typeof(PaginationService<>));

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICipherService, CipherService>();

//Api versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

//JWT
var jwtSection = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection.Issuer,
        ValidAudience = jwtSection.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key))
    };
});
builder.Services.AddAuthorization();

// Adds a security definition named "oauth2" to Swagger, specifying an OAuth 2.0-based authorization scheme.
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme(\"Bearer{token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
