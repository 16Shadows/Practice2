using WebAPP.Areas.Organizers.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebAPP.Areas.Identity.Data;

namespace WebAPP;

public class WebAPPContext : IdentityDbContext<UserAccount>
{
    public WebAPPContext() {}

    public WebAPPContext(DbContextOptions<WebAPPContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        SqliteConnectionStringBuilder sb = new();
        sb.DataSource = "Data\\Database.db";
        sb.ForeignKeys = true;
        sb.Mode = SqliteOpenMode.ReadWriteCreate;

        optionsBuilder.UseSqlite(sb.ToString()).UseLazyLoadingProxies().UseChangeTrackingProxies(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<PageDMO> Pages { get; set; }
    public DbSet<ContainerDMO> Containers { get; set; }
    public DbSet<ObjectDMO> Objects { get; set; }
}
