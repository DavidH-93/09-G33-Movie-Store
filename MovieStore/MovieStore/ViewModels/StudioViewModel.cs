using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MovieStore.ViewModels
{
    public class StudioViewModel
    {
        [Required]
        [Display(Name = "Studio")]

        public string Name { get; set; }
    }
}
