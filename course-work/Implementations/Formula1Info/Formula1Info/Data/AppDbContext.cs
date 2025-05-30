using Formula1Info.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Formula1Info.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Race> Races { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Drivers
            modelBuilder.Entity<Driver>()
                .HasKey(x => x.DriverId);

            modelBuilder.Entity<Driver>()
                .HasOne<Team>()
                .WithMany()
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Driver>()
                .HasIndex(d => d.UniqueNumber)
                .IsUnique();

            #endregion

            #region Teams
            modelBuilder.Entity<Team>()
                .HasKey(x => x.TeamId);
            #endregion

            #region Races
            modelBuilder.Entity<Race>()
                .HasKey(x => x.RaceId);

            modelBuilder.Entity<Race>()
                .HasOne<Driver>()
                .WithMany()
                .HasForeignKey(x => x.DriverId)
                .OnDelete(DeleteBehavior.SetNull);
            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             base.OnConfiguring(optionsBuilder);
             optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
