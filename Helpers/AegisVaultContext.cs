using AegisVault.Models.Database;
using Microsoft.EntityFrameworkCore;

public class AegisVaultContext : DbContext {
    public AegisVaultContext(DbContextOptions<AegisVaultContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreateLinkDatabase>()
            .HasKey(l => l.DbId);
    }

    public DbSet<CreateLinkDatabase> Links { get; set; }
}