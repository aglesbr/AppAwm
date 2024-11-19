using AppAwm.Hasit.DataBase.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AppAwm.Hasit.DataBase.DAL
{
    public class DbConnect : DbContext
    {
        public DbConnect() : base() { }

        public DbConnect(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json");
                var config = builder.Build();

                string _urlBase = config.GetSection("ConnectionStrings:WAConnection").Value!;
                //_urlBase = Environment.GetEnvironmentVariable(_urlBase);
                optionsBuilder.UseSqlServer(_urlBase, x => x.MigrationsHistoryTable("__EFMigrationsHistory"));
                base.OnConfiguring(optionsBuilder);
            }

        }

        public virtual DbSet<Anexo> Anexos { get; set; }

    }
}
