using Domein.Interface;
using Domein.Models;
using Infrastructure.DataContext;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<IContext, Context>();
builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<IOrder, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Rest API"));
}

app.UseHttpsRedirection();




app.MapControllers();
app.Run();


