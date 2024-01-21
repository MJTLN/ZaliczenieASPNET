using System;
using Microsoft.EntityFrameworkCore;

namespace OrderMenage.Models
{
    public class OrderDbContext : DbContext
    {

        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
