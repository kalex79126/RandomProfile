using Microsoft.EntityFrameworkCore;
using RandomProfileAPI.Models;
using System.Numerics;

namespace RandomProfileAPI.Data
{
    public class RandomProfileContext : DbContext
    {

        public RandomProfileContext(DbContextOptions<RandomProfileContext> options)
        : base(options)
        {

        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProfileImage>()
                .HasMany(p => p.Profiles)
                .WithOne(d => d.ProfileImage)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
