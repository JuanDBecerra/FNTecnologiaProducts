using Microsoft.EntityFrameworkCore;
using Prueba.Domain.Entities.Model;

namespace Prueba.Infraestructure.Contexts.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Status> Status { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Products> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>()
                .HasKey(ss => new { ss.Sta_Id });

            modelBuilder.Entity<Category>()
               .HasKey(ss => new { ss.Cat_Id });

            modelBuilder.Entity<Products>()
               .HasKey(ss => new { ss.Pro_Id });

            modelBuilder.Entity<Products>()
            .HasOne(sp => sp.Category)
            .WithMany(s => s.Products)
            .HasForeignKey(sp => sp.Pro_CategoryId)
            .HasConstraintName("FK_Products_Category");

            modelBuilder.Entity<Products>()
            .HasOne(sp => sp.Status)
            .WithMany(s => s.Products)
            .HasForeignKey(sp => sp.Pro_StatusId)
            .HasConstraintName("FK_Products_Status");
        }
    }
}