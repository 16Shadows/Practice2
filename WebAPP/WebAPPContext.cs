using WebAPP.Areas.Organizers.Data;
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
        //Ensure directory exists
        Directory.CreateDirectory("Data");

        SqliteConnectionStringBuilder sb = new();
        sb.DataSource = "Data\\Database.db";
        sb.ForeignKeys = true;
        sb.Mode = SqliteOpenMode.ReadWriteCreate;

        optionsBuilder.UseSqlite(sb.ToString());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<CategoryBase>()
               .UseTphMappingStrategy();
        
        builder.Entity<CategoryBase>()
               .HasMany(c => c.Subcategories)
               .WithOne(c => c.Parent)
               .HasForeignKey(c => c.ParentId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.Entity<CategoryBase>()
               .HasMany(c => c.Documents)
               .WithOne(d => d.Category)
               .HasForeignKey(d => d.CategoryId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.Entity<CategoryBase>()
               .HasMany(c => c.Books)
               .WithOne(b => b.ParentCategory)
               .HasForeignKey(b => b.ParentCategoryId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();


        builder.Entity<UserAccount>()
               .HasMany(u => u.Organizers)
               .WithOne(o => o.Owner)
               .HasForeignKey(o => o.OwnerId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();


        builder.Entity<Category>()
               .HasOne(c => c.Organizer)
               .WithMany()
               .HasForeignKey(c => c.OrganizerId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();


        builder.Entity<SectionBase>()
               .UseTphMappingStrategy();

        builder.Entity<SectionBase>()
               .HasMany(s => s.Sections)
               .WithOne(s => s.Parent)
               .HasForeignKey(s => s.ParentId)
               .IsRequired();

        builder.Entity<SectionBase>()
               .HasOne(s => s.Organizer)
               .WithMany()
               .HasForeignKey(s => s.OrganizerId)
               .IsRequired();


        builder.Entity<Document>()
               .HasMany(d => d.Tags)
               .WithMany(t => t.Documents);
    }

    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<PageDMO> Pages { get; set; }
    public DbSet<ContainerDMO> Containers { get; set; }
    public DbSet<ObjectDMO> Objects { get; set; }
    public DbSet<Tag> Tags { get; set; }
}
