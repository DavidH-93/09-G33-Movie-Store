using System;
using Microsoft.AspNetCore.Identity;

namespace MovieStore.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Type { get; set; }

        public Guid AddressID { get; set; }
    }
}
