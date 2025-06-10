using CORE.Entities;
using CORE.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class DataContext:IdentityDbContext<ApplicationUser>
	{
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BrandCategory>()
				.HasKey(i => new { i.CategoryId, i.BrandId });

            modelBuilder.Entity<ApplicationUser>()
                .Property(i => i.Name)
                .HasColumnOrder(0);

            //modelBuilder.Entity<ApplicationUser>()
            //        .Property(i => i.UserName)
            //        .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

		public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandCategory> BrandCategories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Slider> Slider { get; set; }
    }
}
