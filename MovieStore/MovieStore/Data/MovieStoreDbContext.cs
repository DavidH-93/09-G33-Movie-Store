using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using MovieStore.Models;
namespace MovieStore.Data
{
    public class MovieStoreDbContext : IdentityDbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Locality> Locality { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<PostCode> PostCode { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; } 

        public DbSet<AccessLog> AccessLog { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MovieStore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
