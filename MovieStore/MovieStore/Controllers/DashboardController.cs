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
    public class DashboardController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMovieRepository _movieRepo;
        private readonly IUserRepository _userRepo;
        private readonly MovieStoreDbContext _context;

        public DashboardController(UserManager<User> userManager, SignInManager<User> signInManager, IMovieRepository movieRepo, IUserRepository userRepo, MovieStoreDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _movieRepo = movieRepo;
            _userRepo = userRepo;
            _context = context;
        }

        public IActionResult Index()
        {
            DashboardViewModel vm = new DashboardViewModel();

            DashboardMovieViewModel movievm;
            List<DashboardMovieViewModel> movievms = new List<DashboardMovieViewModel>();

            List<Movie> movies = _movieRepo.GetAll().ToList<Movie>();
            foreach (Movie movie in movies)
            {
                movievm = new DashboardMovieViewModel()
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DashboardCreateMovieViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Movie movie = new Movie()
                {
                    MovieID = new Guid(),
                    Title = vm.Title,
                    Description = vm.Description,
                    Duration = vm.Duration,
                    Genre = vm.Genre,
                    Price = vm.Price,
                    Rating = vm.Rating,
                    Release = vm.Release,
                    Stock = vm.Stock
                };
                _movieRepo.Create(movie);
                return RedirectToAction("Index");
            }
            return View(vm);

        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            DashboardEditMovieViewModel vm;
            Movie movie = _movieRepo.GetSingle(m => m.MovieID == id);
            vm = new DashboardEditMovieViewModel()
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
        [HttpPost]
        public IActionResult Edit(DashboardEditMovieViewModel vm, Guid id)
        {
            if (ModelState.IsValid)
            {
                Movie movie = _movieRepo.GetSingle(m => m.MovieID == id);
                movie.Title = vm.Title;
                movie.Description = vm.Description;
                movie.Price = vm.Price;
                movie.Stock = vm.Stock;
                movie.Release = vm.Release;
                movie.Duration = vm.Duration;
                movie.Rating = vm.Rating;
                movie.Genre = vm.Genre;

                _movieRepo.Update(movie);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        public IActionResult SearchTitle(string title)
        {
            List<DashboardMovieViewModel> vms = new List<DashboardMovieViewModel>();
            DashboardMovieViewModel vm;
            IEnumerable<Movie> movies = _movieRepo.GetAll();
            if (!String.IsNullOrEmpty(title))
            {
                movies = movies.Where(s => s.Title.Contains(title));
            }
            foreach (Movie movie in movies)
            {
                vm = new DashboardMovieViewModel()
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
            List<DashboardMovieViewModel> vms = new List<DashboardMovieViewModel>();
            DashboardMovieViewModel vm;
            IEnumerable<Movie> movies = _movieRepo.GetAll();
            if (!String.IsNullOrEmpty(genre))
            {
                movies = movies.Where(s => s.Genre.Contains(genre));
            }
            foreach (Movie movie in movies)
            {
                vm = new DashboardMovieViewModel()
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

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            DashboardMovieViewModel vm;
            Movie movie = _movieRepo.GetSingle(m => m.MovieID == id);
            vm = new DashboardMovieViewModel()
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

        [HttpPost]
        public IActionResult DeleteConfirmed(Guid id)
        {
            Movie movie = _movieRepo.GetSingle(m => m.MovieID == id);
            _movieRepo.Delete(movie);
            return RedirectToAction("Index");
        }

    }
}
