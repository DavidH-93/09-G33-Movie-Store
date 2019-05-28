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
            MovieViewModel vm = new MovieViewModel();
            List<MovieViewModel> lvm = new List<MovieViewModel>();
            List<Movie> movies = _movieRepo.GetAll().ToList<Movie>();
            foreach (Movie movie in movies)
            {
                vm.MovieID = movie.MovieID;
                vm.Title = movie.Title;
                vm.Description = movie.Description;
                vm.Duration = movie.Duration;
                vm.Price = movie.Price;
                vm.Quantity = movie.Quantity;
                lvm.Add(vm);
            }
            if (lvm.Count() > 0)
            {
                return View(lvm);
            }
            return View();
        }

    }
}
