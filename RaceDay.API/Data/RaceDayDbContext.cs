using Microsoft.EntityFrameworkCore;
using RaceDay.API.Models;

namespace RaceDay.API.Data
{
    public class RaceDayDbContext : DbContext
    {
        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<EventEnrolment> EventEnrolments { get; set; }
        public DbSet<EventRoute> EventRoutes { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organiser)
                .WithMany()
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Categories)
                .HasForeignKey(c => c.EventID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventRoute>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Routes)
                .HasForeignKey(r => r.EventID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .Property(c => c.EntryFee)
                .HasPrecision(10, 2);
        }
    }
}