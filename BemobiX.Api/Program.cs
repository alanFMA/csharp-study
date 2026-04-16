using BemobiX.Domain.Interfaces;
using BemobiX.Infrastructure.ExternalServices;
// 1. Adicione os dois usings abaixo:
using Microsoft.EntityFrameworkCore;
using BemobiX.Infrastructure.Data;
using BemobiX.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- 1. REGISTRO DE SERVIÇOS (Container DI) ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); 
builder.Services.AddScoped<ILegacyBillingService, Vb6BillingAdapter>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

// 2. A CORREÇÃO: Adicione o registro do banco de dados AQUI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- 2. CONSTRUÇÃO DA APLICAÇÃO ---
var app = builder.Build();

// --- 3. PIPELINE DE REQUISIÇÃO (A ordem importa!) ---
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// AVISO IMPORTANTE: Mapeia as rotas HTTP para os Controllers registrados
app.MapControllers(); 

app.Run();