using Cars.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cars.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Price)
                    .HasPrecision(18, 2);
                
                entity.Property(e => e.Notes)
                    .HasMaxLength(500);
            });
        }
    }
}
