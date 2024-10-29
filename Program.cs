using E_Restaurant.Context;
using E_Restaurant.Implementation;
using E_Restaurant.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the database connection
builder.Services.AddDbContext<RestaurantDbContext>(con => con.UseSqlServer(
    "Data Source=DESKTOP-G6TNHE4\\SQLEXPRESS;Initial Catalog=Restaurant;Integrated Security=True;Trust Server Certificate=True"));

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

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
