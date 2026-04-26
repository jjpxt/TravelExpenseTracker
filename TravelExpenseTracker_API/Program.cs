using Microsoft.EntityFrameworkCore;
using TravelExpenseTracker_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Trips"));
});

var app = builder.Build();

AutoDbMigrate(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void AutoDbMigrate(IServiceProvider sp)
{
    var scope = sp.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    if (context.Database.GetPendingMigrations().Any())
        context.Database.Migrate();
}