using GerenciamentoFrotaVeiculo.Api.Business;
using GerenciamentoFrotaVeiculo.Api.Business.Implamentations;
using GerenciamentoFrotaVeiculo.Api.Data.Contract;
using GerenciamentoFrotaVeiculo.Api.Data.Implementation;
using GerenciamentoFrotaVeiculo.Api.Generic;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Enricher;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Filters;
using GerenciamentoFrotaVeiculo.Api.Hypermedia.Helpers;
using GerenciamentoFrotaVeiculo.Api.Repository;
using GerenciamentoFrotaVeiculo.Api.Repository.IRepository;
using GerenciamentoFrotaVeiculo.Context;
using GerenciamentoFrotaVeiculo.Data.ValueObject;
using GerenciamentoFrotaVeiculo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MySqlContext>(option =>
    option.UseMySql(builder.Configuration["ConnectionStrings:MySqlConnectionString"],
    new MySqlServerVersion(new Version(8, 0, 27))));

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
})
.AddXmlDataContractSerializerFormatters();

builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("x-api-version"),
        new UrlSegmentApiVersionReader());
});

// Versioned API Explorer (para integração com Swagger)
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Formato "v1", "v2", etc.
    options.SubstituteApiVersionInUrl = true;
});

// Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// Seus serviços e injeções
builder.Services.AddSingleton<ColaboradorEnricher>();
builder.Services.AddSingleton<VeiculoEnricher>();
builder.Services.AddSingleton<ColaboradorVeiculoEnricher>();

builder.Services.AddSingleton(provider =>
{
    var options = new HyperMediaFilterOptions();

    options.ContentResponseEnricherList.Add(provider.GetRequiredService<ColaboradorEnricher>());
    options.ContentResponseEnricherList.Add(provider.GetRequiredService<VeiculoEnricher>());
    options.ContentResponseEnricherList.Add(provider.GetRequiredService<ColaboradorVeiculoEnricher>());

    return options;
});

builder.Services.AddSingleton<HyperMediaLinkBuilder>();

// Configurações dos serviços responsáveis por fazer o Parse entre "Model" e "VO"
builder.Services.AddSingleton<IParser<Colaborador, ColaboradorVO>, ColaboradorConverter<ColaboradorVO>>();
builder.Services.AddSingleton<IParser<ColaboradorVO, Colaborador>, ColaboradorConverter<Colaborador>>();
builder.Services.AddSingleton<IParser<VeiculoVO, Veiculo>, VeiculoConverter<Veiculo>>();
builder.Services.AddSingleton<IParser<Veiculo, VeiculoVO>, VeiculoConverter<VeiculoVO>>();
builder.Services.AddSingleton<IParser<ColaboradorVeiculo, ColaboradorVeiculoVO>, ColaboradorVeiculoConverter<ColaboradorVeiculoVO>>();
builder.Services.AddSingleton<IParser<ColaboradorVeiculoVO, ColaboradorVeiculo>, ColaboradorVeiculoConverter<ColaboradorVeiculo>>();

builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<VeiculoRepository>();
builder.Services.AddScoped<ColaboradorRepository>();
builder.Services.AddScoped<ColaboradorVeiculoRepository>();
builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
builder.Services.AddScoped<IColaboradorVeiculoRepository, ColaboradorVeiculoRepository>();

builder.Services.AddScoped<IColaboradorBusiness, ColaboradorBusiness>();
builder.Services.AddScoped<IColaboradorVeiculoBusiness, ColaboradorVeiculoBusiness>();
builder.Services.AddScoped<IVeiculoBusiness, VeiculoBusiness>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });

    var option = new RewriteOptions();
    option.AddRedirect("^$", "swagger");
    app.UseRewriter(option);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaultApi", "api/v{version:apiVersion}/{controller=Values}/{id?}");

app.Run();


// Classe para configurar o Swagger para múltiplas versões
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = "Gerenciamento frota veículos",
                Version = description.ApiVersion.ToString(),
                Description = "API de gerenciamento de frota de veículos",
                Contact = new OpenApiContact
                {
                    Name = "Moisés do Espírito Santo Silva",
                    Email = "meu_email@outlook.com",
                    Url = new Uri("https://github.com/moisesess7")
                }
            });
        }
    }
}
