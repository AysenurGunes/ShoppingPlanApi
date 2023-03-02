using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Extensions;
using ShoppingPlanApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//public static JwtConfig JwtConfig { get; private set; }
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ShoppingPlanDbContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddCustomizeSwagger();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
