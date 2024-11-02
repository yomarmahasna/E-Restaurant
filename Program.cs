using E_Restaurant.Context;
using E_Restaurant.Implementation;
using E_Restaurant.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the database connection
builder.Services.AddDbContext<RestaurantDbContext>(con => con.UseSqlServer(
    "Data Source=DESKTOP-G6TNHE4\\SQLEXPRESS;Initial Catalog=ERestaurant;Integrated Security=True;Trust Server Certificate=True"));

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
//Configure Serilog 
// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
//string loggerPath = configuration.GetSection("LoggerPath").Value;
Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).
                WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "Logs/HRLogging.txt"), rollingInterval: RollingInterval.Day).
                CreateLogger();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
