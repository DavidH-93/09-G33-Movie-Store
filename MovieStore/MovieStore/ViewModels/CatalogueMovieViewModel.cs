using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.ViewModels
{
    public class CatalogueMovieViewModel
    {
        public Guid MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ReleaseDate { get; set; }

        public List<CatalogueGenreViewModel> Genres { get; set; }
        public List<CatalogueActorViewModel> Cast { get; set; }
        public List<CatalogueDirectorViewModel> Directors { get; set; }
        public List<CatalogueProducerViewModel> Producers { get; set; }
        public CatalogueStudioViewModel Studio { get; set; }  

    }
}
