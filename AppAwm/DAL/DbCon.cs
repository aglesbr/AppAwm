using AppAwm.Models;
using Microsoft.EntityFrameworkCore;

namespace AppAwm.DAL
{

    public class DbCon : DbContext
    {
        // ESSE CONSTRUTOR, DESTINA-SE PARA AS MIGRATIONS DAS ENTIDADES,
        // SEM ESSE CONSTRUTOR, NÃO SERÁ POSSIVEL USAR OS COMANDOS Add-Migrations, Remove-Migration, Update-Database
        public DbCon(DbContextOptions<DbCon> option) : base(option) { }

        public DbCon() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                var appSeting = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Staging";

                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.{appSeting}.json", true);

                var config = builder.Build();

                string _urlBase = config.GetSection("ConnectionStrings:WAConnection").Value!;
                optionsBuilder.UseSqlServer(_urlBase, x => x.MigrationsHistoryTable("__EFMigrationsHistory"));
                base.OnConfiguring(optionsBuilder);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Empresa>()
            .HasMany(e => e.Anexos)
            .WithOne(e => e.Empresa)
            .HasForeignKey(e => e.Cd_Empresa_Id)
            .IsRequired(false);

            modelBuilder.Entity<Colaborador>()
            .HasMany(e => e.Anexos)
            .WithOne(e => e.Colaborador)
            .HasForeignKey(e => e.Cd_Funcionario_Id)
            .IsRequired(false);

            modelBuilder.Entity<Empresa>()
                .HasMany(e => e.Funcionarios)
                .WithOne(e => e.Empresa)
                .HasForeignKey(e => e.Id_Empresa)
                .IsRequired(false);

            modelBuilder.Entity<Empresa>()
               .HasMany(e => e.Obras)
               .WithOne(e => e.Empresa)
               .HasForeignKey(e => e.Cd_Empresa_Id)
               .IsRequired(false);

            modelBuilder.Entity<Colaborador>()
               .HasMany(e => e.VinculoObras)
               .WithOne(e => e.Colaborador)
               .HasForeignKey(e => e.Cd_Funcionario_Id)
               .IsRequired(false);

            modelBuilder.Entity<Cargo>()
                .HasMany(e => e.DocumentoComplementar)
                .WithOne(e => e.Cargo)
                .HasForeignKey(e => e.Cd_Cargo_Id)
                .IsRequired(false);

            modelBuilder.Entity<Cliente>()
               .HasMany(e => e.Empresas)
               .WithOne(e => e.Cliente)
               .HasForeignKey(e => e.Cd_Cliente_Id)
               .IsRequired(false);
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Colaborador> Funcionarios { get; set; }
        public virtual DbSet<Cargo> Cargos { get; set; }
        public virtual DbSet<Anexo> Anexos { get; set; }
        public virtual DbSet<Obra> Obras { get; set; }
        public virtual DbSet<HistoricoExecucao> HistoricoExecucoes { get; set; }
        public virtual DbSet<DocumentacaoComplementar> DocumentacoesComplementares { get; set; }
        public virtual DbSet<DocumentacaoCargo> DocumentacaoCargos { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }

    }
}
