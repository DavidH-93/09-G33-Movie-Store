using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MovieStore.ViewModels
{
    public class GenreViewModel
    {
        public Guid GenreID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
