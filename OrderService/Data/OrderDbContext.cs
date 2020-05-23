using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data
{
    public class OrderDbContext :DbContext
    {

        
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }       
        public DbSet<Order> Orders { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Order>().HasData(
            //    new Order()
            //    {
            //        OrderId= Guid.Parse("40fdf8ec-6015-4154-b361-b0f3c231696b"),
            //        ProductId=Guid.Parse("169d6506-d906-48de-ac2f-17205cac6167"),
            //        OrderDate=DateTime.Now

            //    }
            //    );
        }
    }
}
