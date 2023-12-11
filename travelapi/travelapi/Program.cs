using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using travelapi.Infrastructure; // Certifique-se de ter o namespace correto aqui
using System;
using Microsoft.AspNetCore.Hosting;
using travelapi.Application.Interfaces;
using travelapi.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IActivityServices, ActivityServices>();
builder.Services.AddScoped<TravelContext>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("*")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var context = new TravelContext();
//if (context.HasData())
//{
//    Console.WriteLine("O banco de dados já possui dados.");
//}
//else
//{
//    Console.WriteLine("O banco de dados está vazio. Adicionando dados...");
//    context.AddDataIfNotExists();
//}
var initializer = new DatabaseInitializer(context);
initializer.AddDataIfNotExists();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
