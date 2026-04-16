using Microsoft.EntityFrameworkCore;
using BemobiX.Domain.Entities;

namespace BemobiX.Infrastructure.Data;

public class AppDbContext : DbContext
{
    // O construtor é essencial para a Injeção de Dependência. 
    // Ele permite que o Program.cs injete a string de conexão e o provedor (PostgreSQL).
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Cada DbSet representa uma tabela física no banco de dados.
    public DbSet<Subscription> Subscriptions { get; set; }

    /// <summary>
    /// Onde a "mágica" da configuração acontece. 
    /// Usamos a Fluent API para manter a Entidade de Domínio limpa de atributos de infraestrutura.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da Entidade Subscription
        modelBuilder.Entity<Subscription>(entity =>
        {
            // Define o nome da tabela (opcional, mas bom para padronização)
            entity.ToTable("subscriptions");

            // Chave Primária
            entity.HasKey(s => s.Id);

            // Configurações de Colunas
            entity.Property(s => s.UserId)
                .IsRequired();

            entity.Property(s => s.PlanName)
                .HasMaxLength(100)
                .IsRequired();

            // PostgreSQL exige precisão explícita para tipos decimais/monetários
            entity.Property(s => s.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(s => s.IsActive)
                .HasDefaultValue(true);
        });
    }
}