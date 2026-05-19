using Microsoft.EntityFrameworkCore;
using WebApplication5.Entities;

namespace WebApplication5.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Pcs> Pcs { get; set; }
    public DbSet<PcComponents> PCComponents { get; set; }
    public DbSet<Components> Components { get; set; }
    public DbSet<ComponentTypes> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pcs>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).HasMaxLength(50).IsRequired();
            e.Property(p => p.Weight).HasColumnType("float(5)").IsRequired();
            e.Property(p => p.Warranty).IsRequired();
            e.Property(p => p.CreatedAt).HasColumnType("datetime").IsRequired();
            e.Property(p => p.Stock).IsRequired();

            e.HasData(
                new Pcs { Id = 1, Name = "Gaming Beast X", Weight = 12.5m, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
                new Pcs { Id = 2, Name = "Office Mini Pro", Weight = 4.2m, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
                new Pcs { Id = 3, Name = "Workstation Pro", Weight = 8.7m, Warranty = 48, CreatedAt = new DateTime(2026, 3, 1, 10, 0, 0), Stock = 3 }
            );
        });

        modelBuilder.Entity<ComponentManufacturer>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.Abbreviation).HasMaxLength(30).IsRequired();
            e.Property(m => m.FullName).HasMaxLength(300).IsRequired();
            e.Property(m => m.FoundationDate).HasColumnType("date").IsRequired();

            e.HasData(
                new ComponentManufacturer { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateTime(1969, 5, 1) },
                new ComponentManufacturer { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateTime(1993, 4, 5) },
                new ComponentManufacturer { Id = 3, Abbreviation = "COR", FullName = "Corsair Gaming Inc.", FoundationDate = new DateTime(1994, 1, 1) }
            );
        });

        modelBuilder.Entity<ComponentTypes>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Abbreviation).HasMaxLength(30).IsRequired();
            e.Property(t => t.Name).HasMaxLength(150).IsRequired();

            e.HasData(
                new ComponentTypes { Id = 1, Abbreviation = "CPU", Name = "Processor" },
                new ComponentTypes { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
                new ComponentTypes { Id = 3, Abbreviation = "RAM", Name = "Memory" }
            );
        });

        modelBuilder.Entity<Components>(e =>
        {
            e.HasKey(c => c.Code);
            e.Property(c => c.Code).HasColumnType("char(10)").IsRequired();
            e.Property(c => c.Name).HasMaxLength(300).IsRequired();
            e.Property(c => c.Description).HasColumnType("nvarchar(max)").IsRequired();

            e.HasOne(c => c.ComponentManufacturer)
                .WithMany()
                .HasForeignKey(c => c.ComponentManufacturerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(c => c.ComponentType)
                .WithMany()
                .HasForeignKey(c => c.ComponentTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            e.HasData(
                new Components { Code = "CPU0000001", Name = "Ryzen 7 7800X3D", Description = "8-core gaming processor", ComponentManufacturerId = 1, ComponentTypeId = 1 },
                new Components { Code = "GPU0000001", Name = "RTX 4080 Super", Description = "High-end gaming graphics card", ComponentManufacturerId = 2, ComponentTypeId = 2 },
                new Components { Code = "RAM0000001", Name = "Corsair Vengeance DDR5 16GB", Description = "DDR5 RAM module 16GB", ComponentManufacturerId = 3, ComponentTypeId = 3 }
            );
        });

        modelBuilder.Entity<PcComponents>(e =>
        {
            e.HasKey(pc => new { pc.PcId, pc.ComponentCode });

            e.HasOne(pc => pc.Pc)
                .WithMany(p => p.PcComponents)
                .HasForeignKey(pc => pc.PcId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(pc => pc.Component)
                .WithMany(c => c.PcComponents)
                .HasForeignKey(pc => pc.ComponentCode)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            e.HasData(
                new PcComponents { PcId = 1, ComponentCode = "CPU0000001", Ammount = 1 },
                new PcComponents { PcId = 1, ComponentCode = "GPU0000001", Ammount = 1 },
                new PcComponents { PcId = 1, ComponentCode = "RAM0000001", Ammount = 2 }
            );
        });
    }
}