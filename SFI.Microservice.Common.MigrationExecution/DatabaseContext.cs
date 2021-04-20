using Microsoft.EntityFrameworkCore;
using SFI.Microservice.Common.DatabaseModel;

namespace SFI.Microservice.Common.MigrationExecution
{
    public class DatabaseContext : DbContext
    {
        public DbSet<EventItem> Events { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Participant> Participants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:sfi-server.database.windows.net,1433;Initial Catalog=sfi-database-lead;Persist Security Info=False;User ID=sfiadmin;Password=Test@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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
