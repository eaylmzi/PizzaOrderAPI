

using Microsoft.AspNetCore.Mvc.Versioning;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
