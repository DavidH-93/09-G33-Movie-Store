using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using MovieStore.Models;
using MovieStore.ViewModels;
using MovieStore.Services;
using MovieStore.Data;

namespace MovieStore.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMovieRepository _movieRepo;
        private readonly IUserRepository _userRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly IActorRepository _actorRepo;
        private readonly IDirectorRepository _directorRepo;
        private readonly IProducerRepository _producerRepo;
        private readonly IStudioRepository _studioRepo;
        private readonly IMovieGenreRepository _movieGenreRepo;
        private readonly IMovieActorRepository _movieActorRepo;
        private readonly IMovieDirectorRepository _movieDirectorRepo;
        private readonly IMovieProducerRepository _movieProducerRepo;
        private readonly IMovieStudioRepository _movieStudioRepo;
        private readonly MovieStoreDbContext _context;

        public CatalogueController(UserManager<User> userManager, SignInManager<User> signInManager, IMovieRepository movieRepo, IUserRepository userRepo, IGenreRepository genreRepo, IActorRepository actorRepo, IDirectorRepository directorRepo, IProducerRepository producerRepo, IStudioRepository studioRepo, IMovieGenreRepository movieGenreRepo, IMovieActorRepository movieActorRepo, IMovieDirectorRepository movieDirectorRepo, IMovieProducerRepository movieProducerRepo, IMovieStudioRepository movieStudioRepo, MovieStoreDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _movieRepo = movieRepo;
            _userRepo = userRepo;
            _genreRepo = genreRepo;
            _actorRepo = actorRepo;
            _directorRepo = directorRepo;
            _producerRepo = producerRepo;
            _studioRepo = studioRepo;
            _movieGenreRepo = movieGenreRepo;
            _movieActorRepo = movieActorRepo;
            _movieDirectorRepo = movieDirectorRepo;
            _movieProducerRepo = movieProducerRepo;
            _movieStudioRepo = movieStudioRepo;
            _context = context;
        }


        //INDEX
        //Returns a customer view for browsing, and interacting with content.
        //List Featured Movies
        //List Popular Movies
        //List By Genre
        //List Actors
        //List By Director
        //List By Producer
        //List By Studio

        public IActionResult Index()
        {
            CatalogueViewModel vm = new CatalogueViewModel();

            CatalogueMovieViewModel movievm = new CatalogueMovieViewModel();
            CatalogueGenreViewModel genrevm = new CatalogueGenreViewModel();
            CatalogueActorViewModel actorvm = new CatalogueActorViewModel();
            CatalogueDirectorViewModel directorvm = new CatalogueDirectorViewModel();
            CatalogueProducerViewModel producervm = new CatalogueProducerViewModel();
            CatalogueStudioViewModel studiovm = new CatalogueStudioViewModel();

            List<Movie> movies = _movieRepo.GetAll().ToList<Movie>();
            List<Genre> genres = _genreRepo.GetAll().ToList<Genre>();
            List<Actor> actors = _actorRepo.GetAll().ToList<Actor>();
            List<Director> directors = _directorRepo.GetAll().ToList<Director>();
            List<Producer> producers = _producerRepo.GetAll().ToList<Producer>();
            List<Studio> studios = _studioRepo.GetAll().ToList<Studio>();
            
            foreach (Movie movie in movies)
            {
                movievm.MovieID = movie.MovieID;
                movievm.Title = movie.Title;
                movievm.Description = movie.Description;
                movievm.Duration = movie.Duration;
                movievm.Price = movie.Price;
                movievm.Quantity = movie.Quantity;
                vm.Featured.Add(movievm);
            }
            foreach (Genre genre in genres)
            {
                genrevm.GenreID = genre.GenreID;
                genrevm.Name = genrevm.Name;
                vm.Genres.Add(genrevm);
            }
            foreach (Actor actor in actors)
            {
                actorvm.ActorID = actor.ActorID;
                actorvm.Gender = actor.Gender;
                actorvm.DOB = actor.DOB;
                actorvm.Name = actor.FirstName + " " + actor.LastName;
                vm.Actors.Add(actorvm);
            }
            foreach (Director director in directors)
            {
                directorvm.DirectorID = director.DirectorID;
                directorvm.Gender = director.Gender;
                directorvm.Name = director.FirstName + " " + director.LastName;
                vm.Directors.Add(directorvm);
            }
            foreach (Producer producer in producers)
            {
                producervm.ProducerID = producer.ProducerID;
                producervm.Gender = producer.Gender;
                producervm.Name = producer.FirstName + " "  + producer.LastName;
                vm.Producers.Add(producervm);
            }
            foreach (Studio studio in studios)
            {
                studiovm.StudioID = studio.StudioID;
                studiovm.Name = studio.Name;
                vm.Studios.Add(studiovm);
            }
            return View(vm);
        }

        public IActionResult Search()
        {
            return View();
        }

    }
}
