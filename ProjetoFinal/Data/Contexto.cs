using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;

namespace ProjetoFinal.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Solicitacao> Solicitacao { get; set; }
        public DbSet<Leito> Leito { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Endereco)
                .WithOne(e => e.Paciente)
                .HasForeignKey<Endereco>(e => e.IdPaciente);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Solicitacoes)
                .WithOne(s => s.Paciente)
                .HasForeignKey(s => s.IdPaciente);

            modelBuilder.Entity<Solicitacao>()
                .HasOne(s => s.Leito)
                .WithOne(l => l.Solicitacao)
                .HasForeignKey<Solicitacao>(s => s.IdLeito);
        }
    }
}
