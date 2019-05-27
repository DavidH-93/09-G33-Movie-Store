using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.ViewModels
{
    public class MovieOrderItemViewModel
    {
        public Guid MovieID { get; set; }
        public string Title { get; set; }
    }
}
