using DMOrganizerDomainModel;
using Microsoft.EntityFrameworkCore;

namespace WebAPP
{
    public class DMOrganizerDBContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Section> Sections { get; set; }
    }
}
