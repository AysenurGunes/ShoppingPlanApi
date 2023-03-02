using Microsoft.EntityFrameworkCore;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ShoppingPlanDbContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

//bu scope yapýlanmasý ile IBook interface ile yönlendirilecek class belirtilir.
builder.Services.AddScoped<IShoppingPlan<Category>, ShoppingPlanRepositoryBase<Category, ShoppingPlanDbContext>>();
builder.Services.AddScoped<IShoppingPlan<Measurement>, ShoppingPlanRepositoryBase<Measurement, ShoppingPlanDbContext>>();
builder.Services.AddScoped<IShoppingPlan<Product>, ShoppingPlanRepositoryBase<Product, ShoppingPlanDbContext>>();
builder.Services.AddScoped<IShoppingPlan<Role>, ShoppingPlanRepositoryBase<Role, ShoppingPlanDbContext>>();
builder.Services.AddScoped<IShoppingPlan<ShoppingList>, ShoppingPlanRepositoryBase<ShoppingList, ShoppingPlanDbContext>>();
builder.Services.AddScoped<IShoppingPlan<ShoppingListDetail>, ShoppingPlanRepositoryBase<ShoppingListDetail, ShoppingPlanDbContext>>();
builder.Services.AddScoped<IShoppingPlan<User>, ShoppingPlanRepositoryBase<User, ShoppingPlanDbContext>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{

    var db = scope.ServiceProvider.GetRequiredService<ShoppingPlanDbContext>();
    try
    {
        db.Database.Migrate();
        var services = scope.ServiceProvider;
        DataGenerator.Initialize(services);

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    var pendings = db.Database.GetPendingMigrations().ToList();

}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
