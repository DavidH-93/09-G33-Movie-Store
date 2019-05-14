using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MovieStore.ViewModels
{
    public class MovieViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Duration")]
        public TimeSpan Duration { get; set; }
        [Required]
        [Display(Name = "Price")]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public List<GenreViewModel> Genres { get; set; }
        [Required]
        [Display(Name ="Actor")]
        public List<ActorViewModel> Actors { get; set; }
        [Required]
        [Display(Name ="Director")]
        public List<DirectorViewModel> Directors { get; set; }
        [Required]
        [Display(Name = "Producer")]
        public List<ProducerViewModel> Producers { get; set; }
        [Required]
        [Display(Name = "Studio")]
        public List<StudioViewModel> Studios { get; set; }
    }
}
