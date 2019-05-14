using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using MovieStore.Models;
using MovieStore.ViewModels;
using MovieStore.Services;


namespace MovieStore.Controllers
{
    public class MovieController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IMovieRepository _movieRepo;
        private IUserRepository _userRepo;
        private IGenreRepository _genreRepo;
        private IMovieGenreRepository _movieGenreRepo;


        public MovieController(UserManager<User> userManager, SignInManager<User> signInManager, IMovieRepository movieRepo, IUserRepository userRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _movieRepo = movieRepo;
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            //get Existing genres/directors/actors/studios and allow search
            //on this data in form controls
            //jquery dynamic add to list
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(MovieViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var u = await GetCurrentUserAsync();

                Movie movie;
                Genre genre;
                List<Genre> genres = new List<Genre>();
                MovieGenre movieGenre;
                Actor actor;
                MovieActor movieActor;
                Director director;
                MovieDirector movieDirector;
                Studio studio;
                MovieStudio movieStudio;

                foreach (GenreViewModel genrevm in vm.Genres)
                {
                    genre = new Genre()
                    {
                        GenreID = new Guid(),
                        Name = genrevm.Name
                    };
                    genres.Add(genre);
                    _genreRepo.Create(genre);
                }
                foreach (ActorViewModel actorvm in vm.Actors)
                {
                    actor = new Actor()
                    {
                        ActorID = new Guid(),
                        FirstName = actorvm.FirstName,
                        LastName = actorvm.LastName,
                        DOB = actorvm.DOB
                    };
                }

                movie = new Movie()
                {
                    MovieID = new Guid(),
                    Title = vm.Title,
                    Price = vm.Price,
                    Quantity = vm.Quantity,
                    ReleaseDate = vm.ReleaseDate,
                    Duration = vm.Duration
                };

                
                

                

                
                return View("Index");

            }
            return View(vm);
        }

        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}
