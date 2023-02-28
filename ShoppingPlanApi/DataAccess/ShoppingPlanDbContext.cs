using Microsoft.EntityFrameworkCore;
using ShoppingPlanApi.Models;

namespace ShoppingPlanApi.DataAccess
{
    public class ShoppingPlanDbContext : DbContext
    {
        public ShoppingPlanDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(c => c.UserID).IsRequired();
            modelBuilder.Entity<User>().HasKey(c => c.UserID).IsClustered();
            modelBuilder.Entity<User>().Property(c => c.RoleID).IsRequired();
            modelBuilder.Entity<User>().Property(c => c.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(c => c.Surname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(c => c.Email).IsRequired();
            modelBuilder.Entity<User>().Property(c => c.Password).IsRequired();

            modelBuilder.Entity<Product>().Property(c => c.ProductID).IsRequired();
            modelBuilder.Entity<Product>().HasKey(c => c.ProductID).IsClustered();

            modelBuilder.Entity<Category>().Property(c => c.CategoryID).IsRequired();
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryID).IsClustered();

            modelBuilder.Entity<Measurement>().Property(c => c.MeasurementID).IsRequired();
            modelBuilder.Entity<Measurement>().HasKey(c => c.MeasurementID).IsClustered();

            modelBuilder.Entity<Role>().Property(c => c.RoleID).IsRequired();
            modelBuilder.Entity<Role>().HasKey(c => c.RoleID).IsClustered();

            modelBuilder.Entity<ShoppingList>().Property(c => c.ShoppingListID).IsRequired();
            modelBuilder.Entity<ShoppingList>().HasKey(c => c.ShoppingListID).IsClustered();
            modelBuilder.Entity<ShoppingList>().Property(c => c.CategoryID).IsRequired();
            modelBuilder.Entity<ShoppingList>().Property(c => c.Done).HasDefaultValue(false);
            modelBuilder.Entity<ShoppingList>().Property(c => c.CreatedDate).HasDefaultValue(DateTime.UtcNow);

            modelBuilder.Entity<ShoppingListDetail>().Property(c => c.ShoppingListDetailID).IsRequired();
            modelBuilder.Entity<ShoppingListDetail>().HasKey(c => c.ShoppingListDetailID).IsClustered();
            modelBuilder.Entity<ShoppingListDetail>().Property(c => c.ShoppingListID).IsRequired();
            modelBuilder.Entity<ShoppingListDetail>().Property(c => c.ProductID).IsRequired();
            modelBuilder.Entity<ShoppingListDetail>().Property(c => c.Quantity).IsRequired();
            modelBuilder.Entity<ShoppingListDetail>().Property(c => c.MeasurementID).IsRequired();
            modelBuilder.Entity<ShoppingListDetail>().Property(c => c.Done).HasDefaultValue(false);

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListDetail> ShoppingListDetails { get; set; }
        public DbSet<User> Users { get; set; }
      
    }
}
