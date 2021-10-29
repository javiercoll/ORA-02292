using Application;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Ora02292DbContext : DbContext
    {
        public Ora02292DbContext(DbContextOptions<Ora02292DbContext> options) : base(options)
        {}

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<Vehicle>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Vehicle>().HasOne(x => x.Driver)
                .WithMany(x => x.Vehicles)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Car>().ToTable("Cars");
        }
    }
}