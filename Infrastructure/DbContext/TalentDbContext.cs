

using Microsoft.EntityFrameworkCore;

public class TalentDbContext : DbContext
{
    public TalentDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);

    public DbSet<ProfileEntity>? Users { get; set; }
    public DbSet<NotificationEntity>? Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfileEntity>()
                    .Ignore(x => x.Name);

        modelBuilder.Entity<ProfileEntity>()
                    .OwnsOne(x => x.Name)
                    .Property(x => x.FirstName)
                    .HasColumnName("FirstName");

        modelBuilder.Entity<ProfileEntity>()
                    .OwnsOne(x => x.Name)
                    .Property(x => x.LastName)
                    .HasColumnName("LastName");

        modelBuilder.Entity<NotificationEntity>()
                    .Property(p => p.Id)
                    .HasDefaultValueSql("newid()");

        modelBuilder.Entity<NotificationEntity>()
                    .Property(p => p.CreatedAt)
                    .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<NotificationEntity>()
                    .HasKey(p => p.Id)
                    .IsClustered(false);

        modelBuilder.Entity<ProfileEntity>()
                    .Property(p => p.Id)
                    .HasDefaultValueSql("newid()");

        modelBuilder.Entity<ProfileEntity>()
                    .Property(p => p.CreatedAt)
                    .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<ProfileEntity>()
                    .HasKey(p => p.Id)
                    .IsClustered(false);

    }
}