

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using pizzaorder.Data.Repositories.Baskets;
using pizzaorder.Data.Repositories.Discounts;
using pizzaorder.Data.Repositories.Ingredients;
using pizzaorder.Data.Repositories.Orders;
using pizzaorder.Data.Repositories.PizzaIngredients;
using pizzaorder.Data.Repositories.Pizzas;
using pizzaorder.Data.Repositories.Users;
using pizzaorder.Logic.Logics.Baskets;
using pizzaorder.Logic.Logics.Discounts;
using pizzaorder.Logic.Logics.Ingredients;
using pizzaorder.Logic.Logics.Orders;
using pizzaorder.Logic.Logics.PizzaIngredients;
using pizzaorder.Logic.Logics.Pizzas;
using pizzaorder.Logic.Logics.Users;
using PizzaOrderAPI.Services.SecurityServices.Jwt.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
