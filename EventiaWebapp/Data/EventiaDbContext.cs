using EventiaWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Data
{
    public class EventiaDbContext : DbContext
    {

        public EventiaDbContext(DbContextOptions<EventiaDbContext> options) : base(options)
        {

        }

        public DbSet<Attendee> Attendes { get; set; }
        public DbSet<Event> Events { get; set; }

        public DbSet<Organizer> Organizers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>().ToTable("Attende");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Organizer>().ToTable("Organizer");
        }
    }
}
