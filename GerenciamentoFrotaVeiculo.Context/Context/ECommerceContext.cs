using GerenciamentoFrotaVeiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Context
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
        }

        public DbSet<Colaborador> Colaboradores { get; set; } = null!;
        public DbSet<Veiculo> Veiculos { get; set; } = null!;
        public DbSet<ColaboradorVeiculo> ColaboradoresVeiculos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colaborador>()
                .HasMany(c => c.Veiculos).WithMany(v => v.Colaboradores)
                .UsingEntity<ColaboradorVeiculo>(
                cv => cv.HasOne(cv => cv.Veiculo).WithMany(v => v.ColaboradoresVeiculos).HasForeignKey(fk => fk.VeiculoId),
                cv => cv.HasOne(cv => cv.Colaborador).WithMany(c => c.ColaboradoresVeiculos).HasForeignKey(fk => fk.ColaboradorId),
                cv => cv.HasKey(pk => new { pk.ColaboradorId, pk.VeiculoId })
                );
        }
    }
}
