using System;
using Microsoft.AspNetCore.Identity;

namespace MovieStore.Models
{
    public class Staff : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Position { get; set; }

        public Guid AddressID { get; set; }
    }
}
