using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MovieStore.ViewModels
{
    public class MovieViewModel
    {
        public Guid MovieID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        //[Required]
        //[Display(Name = "Image")]
        //public IFormFile MovieImageUpload { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Duration")]
        public int Duration { get; set; }
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
        public GenreViewModel Genre { get; set; }
        [Required]
        [Display(Name ="Actor")]
        public ActorViewModel Actor { get; set; }
        [Required]
        [Display(Name ="Director")]
        public DirectorViewModel Director { get; set; }
        [Required]
        [Display(Name = "Producer")]
        public ProducerViewModel Producer { get; set; }
        [Required]
        [Display(Name = "Studio")]
        public StudioViewModel Studio { get; set; }
    }
}
