using Microsoft.EntityFrameworkCore;
using ShoppingApiAdmin.Models;

namespace ShoppingApiAdmin.DataAccess
{
    public class ShoppingApiAdminDbContext:DbContext
    {
        public ShoppingApiAdminDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string con = @"Server=DESKTOP-TN9J4B3\SQLEXPRESS; Database=ShoppingApiAdmin;User Id=patika; Password=123patika; TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(con);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AdminShoppingList>().Property(c => c.AdminShoppingListID).IsRequired();
            modelBuilder.Entity<AdminShoppingList>().HasKey(c=>c.AdminShoppingListID).IsClustered();
         
        }
        public DbSet<AdminShoppingList> AdminShoppingLists { get; set; }
    }
}
