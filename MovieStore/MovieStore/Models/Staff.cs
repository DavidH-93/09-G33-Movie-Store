using System;
using Microsoft.AspNetCore.Identity;

namespace MovieStore.Models
{
    public class Staff : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Type { get; set; } // Position

        public Guid AddressID { get; set; }
    }
}
