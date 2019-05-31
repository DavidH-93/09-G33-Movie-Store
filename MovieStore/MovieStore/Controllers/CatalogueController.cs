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
        private readonly MovieStoreDbContext _context;

        public CatalogueController(UserManager<User> userManager, SignInManager<User> signInManager, IMovieRepository movieRepo, IUserRepository userRepo, MovieStoreDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _movieRepo = movieRepo;
            _userRepo = userRepo;
            _context = context;
        }
        public IActionResult Index()
        {
            CatalogueViewModel vm = new CatalogueViewModel();

            CatalogueMovieViewModel movievm;
            List<CatalogueMovieViewModel> movievms = new List<CatalogueMovieViewModel>();

            List<Movie> movies = _movieRepo.GetAll().ToList<Movie>();
            foreach (Movie movie in movies)
            {
                movievm = new CatalogueMovieViewModel()
                {
                    MovieID = movie.MovieID,
                    Title = movie.Title,
                    Description = movie.Description,
                    Duration = movie.Duration,
                    Genre = movie.Genre,
                    Price = movie.Price,
                    Rating = movie.Rating,
                    Release = movie.Release,
                    Stock = movie.Stock
                };
                movievms.Add(movievm);
            }
            vm.Movies = movievms;
            return View(vm);
        }

        public IActionResult Details(Guid id)
        {
            CatalogueMovieViewModel vm;
            Movie movie = _movieRepo.GetSingle(m => m.MovieID == id);
            vm = new CatalogueMovieViewModel()
            {
                MovieID = movie.MovieID,
                Title = movie.Title,
                Description = movie.Description,
                Duration = movie.Duration,
                Genre = movie.Genre,
                Price = movie.Price,
                Rating = movie.Rating,
                Release = movie.Release,
                Stock = movie.Stock
            };
            return View(vm);
        }

        public IActionResult SearchTitle(string title)
        {
            List<CatalogueMovieViewModel> vms = new List<CatalogueMovieViewModel>();
            CatalogueMovieViewModel vm;
            IEnumerable<Movie> movies = _movieRepo.GetAll();
            if (!String.IsNullOrEmpty(title))
            {
                movies = movies.Where(s => s.Title.Contains(title));
            }
            foreach (Movie movie in movies)
            {
                vm = new CatalogueMovieViewModel()
                {
                    MovieID = movie.MovieID,
                    Title = movie.Title,
                    Description = movie.Description,
                    Duration = movie.Duration,
                    Genre = movie.Genre,
                    Price = movie.Price,
                    Rating = movie.Rating,
                    Release = movie.Release,
                    Stock = movie.Stock
                };
                vms.Add(vm);
            }
            return View(vms);
        }

        public IActionResult SearchGenre(string genre)
        {
            List<CatalogueMovieViewModel> vms = new List<CatalogueMovieViewModel>();
            CatalogueMovieViewModel vm;
            IEnumerable<Movie> movies = _movieRepo.GetAll();
            if (!String.IsNullOrEmpty(genre))
            {
                movies = movies.Where(s => s.Genre.Contains(genre));
            }
            foreach (Movie movie in movies)
            {
                vm = new CatalogueMovieViewModel()
                {
                    MovieID = movie.MovieID,
                    Title = movie.Title,
                    Description = movie.Description,
                    Duration = movie.Duration,
                    Genre = movie.Genre,
                    Price = movie.Price,
                    Rating = movie.Rating,
                    Release = movie.Release,
                    Stock = movie.Stock
                };
                vms.Add(vm);
            }
            return View(vms);
        }

    }
}
