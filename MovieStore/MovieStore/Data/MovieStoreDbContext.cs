using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Director> Director { get; set; }

        internal Task GetUserAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

        public DbSet<Producer> Producer { get; set; }
        public DbSet<Studio> Studio { get; set; }
        public DbSet<MovieActor> MovieActor { get; set; }
        public DbSet<MovieDirector> MovieDirector { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<MovieProducer> MovieProducer { get; set; }
        public DbSet<MovieStudio> MovieStudio { get; set; }

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
