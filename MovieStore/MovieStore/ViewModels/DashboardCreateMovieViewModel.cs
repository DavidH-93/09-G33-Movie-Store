using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.ViewModels
{
    public class DashboardCreateMovieViewModel
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        [Range(1, 2000)]
        public int Stock { get; set; }
        [Required]
        public string Release { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }
    }
}
