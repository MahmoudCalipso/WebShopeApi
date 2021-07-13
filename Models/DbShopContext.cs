using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopeApi.Models
{
    public class DbShopContext : DbContext
    {
        public DbShopContext() { }
        public DbShopContext(DbContextOptions<DbShopContext> options)
        : base(options)
        {
        }

       
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<SalesProd> SalesProd { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-9GD4K4L;Database=WebShop;User ID=sa;Password=calipso1996;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



        }
    }
}
