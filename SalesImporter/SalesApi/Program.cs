using Microsoft.EntityFrameworkCore;
using SalesApi.Data;
using SalesApi.Models;
using SalesApi.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

//builder.Services.AddAuthentication();

builder.Services.AddDbContext<SalesDbContext>(options =>options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();



app.Run();
