using EventiaWebapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventiaWebapp.Data
{
    public class EventiaDbContext : IdentityDbContext<User>
    {

        public EventiaDbContext(DbContextOptions options) : base(options) {
        }

        public DbSet<Event> Events { get; set; }

    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(e => e.HostedEvent)
                .WithMany( e => e.Organizer)
                .OnDelete(DeleteBehavior.NoAction);


       

        }
    }
}
