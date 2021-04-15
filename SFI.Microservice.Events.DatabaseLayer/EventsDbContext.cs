using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.DatabaseModel;

namespace SFI.Microservice.Events.DatabaseLayer
{
    public class EventsDbContext : DbContext
    {
        public DbSet<EventItem> Events{ get; set; }
        public DbSet<User> Users { get; set; }

        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventItem>()
                        .ToTable("Events")
                        .HasMany(x => x.Speakers)
                        .WithMany(x => x.Sessions);

            var participantItem = modelBuilder.Entity<Participant>()
                                              .ToTable("Participants");

            participantItem
                .HasOne(x => x.User)
                .WithMany(x => x.Participants);

            participantItem
                .HasOne(x => x.Event)
                .WithMany(x => x.Participants);

            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
