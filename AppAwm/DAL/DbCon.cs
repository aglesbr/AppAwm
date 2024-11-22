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
                    //.AddJsonFile($"appsettings.json", true, true)
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
              .HasOne(e => e.Endereco)
              .WithOne(e => e.Empresa)
              .HasForeignKey<Endereco>(e => e.Cd_Empresa)
              .IsRequired(false);

            modelBuilder.Entity<Empresa>()
            .HasMany(e => e.Anexos)
            .WithOne(e => e.Empresa)
            .HasForeignKey(e => e.Cd_Empresa_Id)
            .IsRequired(false);

            modelBuilder.Entity<Funcionario>()
            .HasMany(e => e.Anexos)
            .WithOne(e => e.Funcionario)
            .HasForeignKey(e => e.Cd_Funcionario_Id)
            .IsRequired(false);

            modelBuilder.Entity<Funcionario>()
             .HasOne(e => e.Endereco)
             .WithOne(e => e.Funcionario)
             .HasForeignKey<Endereco>(e => e.Cd_Funcionario_id)
             .IsRequired(false);

            modelBuilder.Entity<Funcionario>()
           .HasOne(e => e.Cargo)
           .WithOne(e => e.Funcionario)
           .HasForeignKey<Funcionario>(e => e.Cd_Cargo)
           .IsRequired(false); ;

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

            modelBuilder.Entity<Funcionario>()
               .HasMany(e => e.VinculoObras)
               .WithOne(e => e.Funcionario)
               .HasForeignKey(e => e.Cd_Funcionario_Id)
               .IsRequired(false);

            modelBuilder.Entity<Treinamento>()
              .HasMany(e => e.Habilidades)
              .WithOne(e => e.Treinamento)
              .HasForeignKey(f => f.Cd_Treinamento_Id)
              .IsRequired(false);
        }


        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<Funcionario> Funcionarios { get; set; }
        public virtual DbSet<Cargo> Cargos { get; set; }
        public virtual DbSet<Anexo> Anexos { get; set; }
        public virtual DbSet<Obra> Obras { get; set; }
        public virtual DbSet<Treinamento> Treinamentos { get; set; }
        public virtual DbSet<TreinamentoHabilidade> Habilidades { get; set; }
        public virtual DbSet<HistoricoExecucao> HistoricoExecucoes { get; set; }
    }
}
