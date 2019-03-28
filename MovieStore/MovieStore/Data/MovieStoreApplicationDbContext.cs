using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MovieStore.Data
{
    public class MovieStoreApplicationDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-MovieStore-FEF59AEB-10DA-4837-8377-7113ED9537CC;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
