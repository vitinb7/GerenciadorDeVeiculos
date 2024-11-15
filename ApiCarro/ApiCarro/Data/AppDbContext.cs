using ApiCarro.Models;
using ApiCarro.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ApiCarro.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Carro> carros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("DataSource=app.db; Cache=Shared");
        }
    }
}
