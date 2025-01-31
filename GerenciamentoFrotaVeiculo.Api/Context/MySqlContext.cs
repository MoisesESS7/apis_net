using GerenciamentoFrotaVeiculo.Models;
using GerenciamentoFrotaVeiculo.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoFrotaVeiculo.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
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
                cv => cv.HasKey(pk => pk.Id )
                );

            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property<int>("Id").HasColumnOrder(0);
                }
            }
        }
    }
}
