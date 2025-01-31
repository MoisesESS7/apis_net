using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Enricher;
using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Api.Data.Implementation;
using GerenciamentoFrotaVeiculo.Api.Repository;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using GerenciamentoFrotaVeiculo.Api.Generic;
using GerenciamentoFrotaVeiculo.Api.Business;
using GerenciamentoFrotaVeiculo.Api.Business.Implamentations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MySqlContext>(option =>
option.UseMySql(builder.Configuration["ConnectionStrings:MySqlConnectionString"],
new MySqlServerVersion(new Version(8, 0, 27))));
//option.UseSqlServer(builder.Configuration.GetConnectionString("ConectionStringDataBase")));

builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;

    // Suporte a múltiplas formas de enviar a versão
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"), // Ex: ?api-version=2.0
        new HeaderApiVersionReader("x-api-version"), // Ex: X-API-Version: 1.0
        new UrlSegmentApiVersionReader()); // Ex: /api/v2/resource
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var filter = new HyperMediaFilterOptions();
filter.ContentResponseEnricherList.Add(new ColaboradorEnricher());
filter.ContentResponseEnricherList.Add(new VeiculoEnricher());
filter.ContentResponseEnricherList.Add(new ColaboradorVeiculoEnricher());
builder.Services.AddSingleton(filter);

builder.Services.AddSingleton<IParser<Colaborador, ColaboradorVO>, ColaboradorConverter<ColaboradorVO>>();
builder.Services.AddSingleton<IParser<ColaboradorVO, Colaborador>, ColaboradorConverter<Colaborador>>();
builder.Services.AddSingleton<IParser<VeiculoVO, Veiculo>, VeiculoConverter<Veiculo>>();
builder.Services.AddSingleton<IParser<Veiculo, VeiculoVO>, VeiculoConverter<VeiculoVO>>();
builder.Services.AddSingleton<IParser<ColaboradorVeiculo, ColaboradorVeiculoVO>, ColaboradorVeiculoConverter<ColaboradorVeiculoVO>>();
builder.Services.AddSingleton<IParser<ColaboradorVeiculoVO, ColaboradorVeiculo>, ColaboradorVeiculoConverter<ColaboradorVeiculo>>();

builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<ColaboradorRepository>();
builder.Services.AddScoped<IColaboradorVeiculoRepository, ColaboradorVeiculoRepository>();
builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();

builder.Services.AddScoped<IColaboradorBusiness, ColaboradorBusiness>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

app.UseRouting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "api/v{version:apiVersion}/{controller=Values}/{id?}");

app.MapControllers();

app.Run();