using Microsoft.EntityFrameworkCore;
using motorcycle.domain.entities;
using motorcycle.domain.Entities;
using motorcycle.domain.ValueObject;

namespace Motorcycle.App.Api.Context
{
    public class Database : DbContext
    {
        private readonly DbConnection _connection;

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<RentVehicle> RentVehicles { get; set; }
        public DbSet<CNH> CNHs { get; set; }
        public Database(DbConnection connection)
        {
            _connection = connection;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasIndex(item => item.Plate).IsUnique();
            modelBuilder.Entity<Driver>()
                .HasIndex(item => item.CNPJ).IsUnique();
            modelBuilder.Entity<Driver>().HasOne(item => item.CNH);
            
            modelBuilder.Entity<Driver>().HasMany(item => item.RentVehicles);
            modelBuilder.Entity<Vehicle>().HasMany(item => item.RentVehicles);
            Database.Migrate();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connection.GetConnectionString());
        }
    }
}
