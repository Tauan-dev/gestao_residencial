using ApiGastosResidenciais.Application.Common;
using ApiGastosResidenciais.Application.Interfaces;
using ApiGastosResidenciais.Application.Mapping;
using ApiGastosResidenciais.Application.Service;
using ApiGastosResidenciais.Infra.Context;
using ApiGastosResidenciais.Domain.Interfaces;
using ApiGastosResidenciais.Infra.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<DomainToDtoMappingProfile>();
    
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();


builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

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
