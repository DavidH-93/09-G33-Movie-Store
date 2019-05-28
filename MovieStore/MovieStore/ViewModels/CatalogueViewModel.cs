using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.ViewModels
{
    public class CatalogueViewModel
    {
        public List<CatalogueMovieViewModel> Featured { get; set; }
        public List<CatalogueMovieViewModel> Popular { get; set; }
        public List<CatalogueGenreViewModel> Genres { get; set; }
        public List<CatalogueActorViewModel> Actors { get; set; }
        public List<CatalogueDirectorViewModel> Directors { get; set; }
        public List<CatalogueProducerViewModel> Producers { get; set; }
        public List<CatalogueStudioViewModel> Studios { get; set; }

    }
}
