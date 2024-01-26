using Microsoft.EntityFrameworkCore;
using SmartInventory.Models;
using SmartInventory.ViewModels;
using System.Transactions;

namespace SmartInventory.Data
{
    public class imsDbContext:DbContext
    {
        public imsDbContext(DbContextOptions<imsDbContext>option):base(option)
        {
                
        }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryItem> Inventories { get; set; }

        public DbSet<SmartInventory.ViewModels.UserRegistration>? UserRegistration { get; set; }
        public DbSet<SmartInventory.ViewModels.UserLogin>? UserLogin { get; set; }




    }
}
