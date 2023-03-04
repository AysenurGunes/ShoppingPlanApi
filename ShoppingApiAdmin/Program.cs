using Microsoft.EntityFrameworkCore;
using ShoppingApiAdmin.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ShoppingApiAdminDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{

    var db = scope.ServiceProvider.GetRequiredService<ShoppingApiAdminDbContext>();
    try
    {
        db.Database.Migrate();
        var services = scope.ServiceProvider;
        

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
