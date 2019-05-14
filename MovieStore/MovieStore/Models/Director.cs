using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Models
{
    public class Director
    {
        public Guid DirectorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }

        public int Age()
        {
            int age = 0;
            return age = DateTime.Now.Year - DOB.Year;
        }

    }
}
