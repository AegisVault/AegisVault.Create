using AegisVault.Models.Database;
using Microsoft.EntityFrameworkCore;

public class AegisVaultContext : DbContext {
    public AegisVaultContext(DbContextOptions<AegisVaultContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("Store");

        // Entity Specific
        modelBuilder.Entity<CreateLinkDatabase>()
            .HasNoDiscriminator()
            .ToContainer("Links")
            .HasKey(l => l.DbId);
    }

    public DbSet<CreateLinkDatabase> Links { get; set; }
}