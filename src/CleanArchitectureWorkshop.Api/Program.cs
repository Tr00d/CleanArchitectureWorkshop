using CleanArchitectureWorkshop.Application;
using CleanArchitectureWorkshop.Infrastructure;
using FluentValidation.AspNetCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMvc().AddFluentValidation();
builder.Services.AddSwaggerGen();
builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure(builder.Configuration);
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program
{
}