using APIStreamingDeAudio.Data;
using APIStreamingDeAudio.Interfaces;
using APIStreamingDeAudio.Repositories;
using APIStreamingDeAudio.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=streaming.db"));

// Injeção de Dependência (Camadas)
builder.Services.AddScoped<IFaixaAudioRepository, FaixaAudioRepository>();
builder.Services.AddScoped<IFaixaAudioService, FaixaAudioService>();

builder.Services.AddControllers();

// Configuração do Swagger com suporte a XML Comments
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Streaming De Áudio",
        Version = "v1",
        Description = "Web API RESTful para gerenciamento de catálogo de streaming de áudio."
    });

    // Pega o caminho do arquivo XML gerado na compilação para o Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        // Força o status 500 e o tipo de retorno para JSON
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        // Cria um objeto anônimo amigável e seguro para o cliente
        var problemDetails = new
        {
            status = 500,
            title = "Erro Interno do Servidor",
            detail = "Ocorreu uma falha inesperada ao processar sua requisição. Nossa equipe técnica foi notificada."
        };

        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});

// Configuração do Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();