using InvoiceApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Store> Stores { get; set; } = null!;
    public DbSet<Unit> Units { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<InvoiceLine> InvoiceLines { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>().HasMany(i => i.Lines).WithOne().HasForeignKey(l => l.InvoiceId).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
