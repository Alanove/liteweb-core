using Microsoft.EntityFrameworkCore;

namespace lw.Infra.DataContext;
public partial class AppDbContext : DbContext
{
    protected readonly DbContextOptions<AppDbContext> _options;

    public AppDbContext()
    {
    }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        _options = options;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PagePropertyValue>()
            .HasKey(ppv => new { ppv.PageId, ppv.PagePropertiesId });

        modelBuilder.Entity<User>()
           .HasOne(u => u.UserProperties) // User has one UserProperties
           .WithOne(up => up.User)        // UserProperties is associated with one User
           .HasForeignKey<UserProperties>(up => up.UserId); // Foreign key in UserProperties

        modelBuilder.Entity<Page>()
           .HasOne(p => p.User)
           .WithMany(u => u.Pages)
           .HasForeignKey(p => p.CreatedBy); 

        base.OnModelCreating(modelBuilder);
    }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserProperties> UserProperties { get; set; }
    public virtual DbSet<Page> Pages { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Menu> Menus { get; set; }
    public virtual DbSet<Website> Websites { get; set; }
    public virtual DbSet<WebsiteSettings> WebsiteSettings { get; set; }
    public virtual DbSet<PageProperties> PageProperties { get; set; }
    public virtual DbSet<PagePropertyValue> PagePropertyValue { get; set; }
}
