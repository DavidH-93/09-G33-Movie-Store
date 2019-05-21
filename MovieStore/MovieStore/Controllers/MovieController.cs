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

        public MovieController(UserManager<User> userManager, SignInManager<User> signInManager, IMovieRepository movieRepo, IUserRepository userRepo, IGenreRepository genreRepo, IActorRepository actorRepo, IDirectorRepository directorRepo, IProducerRepository producerRepo, IStudioRepository studioRepo, IMovieGenreRepository movieGenreRepo, IMovieActorRepository movieActorRepo, IMovieDirectorRepository movieDirectorRepo, IMovieProducerRepository movieProducerRepo, IMovieStudioRepository movieStudioRepo)
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
        //Add role access authorization
        [HttpPost]
        public async Task<ActionResult> Create(MovieViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //Add role check
                //Add UserMovie to bind  data creation to users
                var u = await GetCurrentUserAsync();

                Movie movie;
                Genre genre;
                MovieGenre movieGenre;
                Actor actor;
                MovieActor movieActor;
                Director director;
                MovieDirector movieDirector;
                Producer producer;
                MovieProducer movieProducer;
                Studio studio;
                MovieStudio movieStudio;

                genre = new Genre()
                {
                    GenreID = new Guid(),
                    Name = vm.Genre.Name
                };
                _genreRepo.Create(genre);

                actor = new Actor()
                {
                    ActorID = new Guid(),
                    FirstName = vm.Actor.FirstName,
                    LastName = vm.Actor.LastName,
                    Gender = vm.Actor.Gender,
                    DOB = vm.Actor.DOB
                };
                _actorRepo.Create(actor);

                director = new Director()
                {
                    DirectorID = new Guid(),
                    FirstName = vm.Director.FirstName,
                    LastName = vm.Director.LastName,
                    Gender = vm.Director.Gender,
                    DOB = vm.Actor.DOB
                };
                _directorRepo.Create(director);

                producer = new Producer()
                {
                    ProducerID = new Guid(),
                    FirstName = vm.Producer.FirstName,
                    LastName = vm.Producer.LastName,
                    Gender = vm.Producer.Gender,
                    DOB = vm.Producer.DOB
                };
                _producerRepo.Create(producer);

                studio = new Studio()
                {
                    StudioID = new Guid(),
                    Name = vm.Studio.Name
                };
                _studioRepo.Create(studio);

                movie = new Movie()
                {
                    MovieID = new Guid(),
                    Title = vm.Title,
                    Description = vm.Description,
                    Price = vm.Price,
                    Quantity = vm.Quantity,
                    ReleaseDate = vm.ReleaseDate,
                    Duration = vm.Duration
                };
                _movieRepo.Create(movie);

                movieGenre = new MovieGenre()
                {
                    MovieGenreID = new Guid(),
                    MovieID = movie.MovieID,
                    GenreID = genre.GenreID
                };
                _movieGenreRepo.Create(movieGenre);

                movieActor = new MovieActor()
                {
                    MovieActorID = new Guid(),
                    MovieID = movie.MovieID,
                    ActorID = actor.ActorID
                };
                _movieActorRepo.Create(movieActor);

                movieDirector = new MovieDirector()
                {
                    MovieDirectorID = new Guid(),
                    MovieID = movie.MovieID,
                    DirectorID = director.DirectorID
                };
                _movieDirectorRepo.Create(movieDirector);

                movieProducer = new MovieProducer()
                {
                    MovieProducerID = new Guid(),
                    MovieID = movie.MovieID,
                    ProducerID = producer.ProducerID
                };
                _movieProducerRepo.Create(movieProducer);

                movieStudio = new MovieStudio()
                {
                    MovieStudioID = new Guid(),
                    MovieID = movie.MovieID,
                    StudioID = studio.StudioID
                };
                _movieStudioRepo.Create(movieStudio);

                return View("Index");

            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Read(Guid id)
        {
            Movie movie = _movieRepo.GetSingle(m => m.MovieID == id);
            MovieGenre movieGenre = _movieGenreRepo.GetSingle(m => m.MovieID == id);
            Genre genre = _genreRepo.GetSingle(g => g.GenreID == movieGenre.GenreID);
            MovieActor movieActor = _movieActorRepo.GetSingle(m => m.MovieID == id);
            Actor actor = _actorRepo.GetSingle(a => a.ActorID == movieActor.ActorID);
            MovieDirector movieDirector = _movieDirectorRepo.GetSingle(m => m.MovieID == id);
            Director director = _directorRepo.GetSingle(d => d.DirectorID == movieDirector.DirectorID);
            MovieProducer movieProducer = _movieProducerRepo.GetSingle(m => m.MovieID == id);
            Producer producer = _producerRepo.GetSingle(p => p.ProducerID == movieProducer.ProducerID);
            MovieStudio movieStudio = _movieStudioRepo.GetSingle(m => m.MovieID == id);
            Studio studio = _studioRepo.GetSingle(s => s.StudioID == movieStudio.StudioID);

            GenreViewModel genrevm = new GenreViewModel()
            {
                GenreID = genre.GenreID,
                Name = genre.Name
            };

            ActorViewModel actorvm = new ActorViewModel()
            {
                ActorID = actor.ActorID,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Gender = actor.Gender,
                DOB = actor.DOB
            };

            DirectorViewModel directorvm = new DirectorViewModel()
            {
                DirectorID = director.DirectorID,
                FirstName = director.FirstName,
                LastName = director.LastName,
                Gender = director.Gender,
                DOB = director.DOB
            };

            ProducerViewModel producervm = new ProducerViewModel()
            {
                ProducerID = producer.ProducerID,
                FirstName = producer.FirstName,
                LastName = producer.LastName,
                Gender = producer.Gender,
                DOB = producer.DOB
            };

            StudioViewModel studiovm = new StudioViewModel()
            {
                StudioID = studio.StudioID,
                Name = studio.Name
            };

            if (movie != null)
            {
                MovieViewModel vm = new MovieViewModel()
                {
                    MovieID = movie.MovieID,
                    Title = movie.Title,
                    Description = movie.Description,
                    Duration = movie.Duration,
                    Price = movie.Price,
                    Quantity = movie.Quantity,
                    ReleaseDate = movie.ReleaseDate,
                    Genre = genrevm,
                    Actor = actorvm,
                    Director = directorvm,
                    Producer = producervm,
                    Studio = studiovm
                };
                return View(vm);
            }
                return View();
        }

        [HttpGet]
        public IActionResult ReadAll()
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
            if(lvm.Count() > 0)
            {
                return View(lvm);
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Movie movie = _movieRepo.GetSingle(m => m.MovieID == id);
            MovieGenre movieGenre = _movieGenreRepo.GetSingle(m => m.MovieID == id);
            Genre genre = _genreRepo.GetSingle(g => g.GenreID == movieGenre.GenreID);
            MovieActor movieActor = _movieActorRepo.GetSingle(m => m.MovieID == id);
            Actor actor = _actorRepo.GetSingle(a => a.ActorID == movieActor.ActorID);
            MovieDirector movieDirector = _movieDirectorRepo.GetSingle(m => m.MovieID == id);
            Director director = _directorRepo.GetSingle(d => d.DirectorID == movieDirector.DirectorID);
            MovieProducer movieProducer = _movieProducerRepo.GetSingle(m => m.MovieID == id);
            Producer producer = _producerRepo.GetSingle(p => p.ProducerID == movieProducer.ProducerID);
            MovieStudio movieStudio = _movieStudioRepo.GetSingle(m => m.MovieID == id);
            Studio studio = _studioRepo.GetSingle(s => s.StudioID == movieStudio.StudioID);

            GenreViewModel genrevm = new GenreViewModel()
            {
                GenreID = genre.GenreID,
                Name = genre.Name
            };

            ActorViewModel actorvm = new ActorViewModel()
            {
                ActorID = actor.ActorID,
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Gender = actor.Gender,
                DOB = actor.DOB
            };

            DirectorViewModel directorvm = new DirectorViewModel()
            {
                DirectorID = director.DirectorID,
                FirstName = director.FirstName,
                LastName = director.LastName,
                Gender = director.Gender,
                DOB = director.DOB
            };

            ProducerViewModel producervm = new ProducerViewModel()
            {
                ProducerID = producer.ProducerID,
                FirstName = producer.FirstName,
                LastName = producer.LastName,
                Gender = producer.Gender,
                DOB = producer.DOB
            };

            StudioViewModel studiovm = new StudioViewModel()
            {
                StudioID = studio.StudioID,
                Name = studio.Name
            };

            if (movie != null)
            {
                MovieViewModel vm = new MovieViewModel()
                {
                    MovieID = movie.MovieID,
                    Title = movie.Title,
                    Description = movie.Description,
                    Duration = movie.Duration,
                    Price = movie.Price,
                    Quantity = movie.Quantity,
                    ReleaseDate = movie.ReleaseDate,
                    Genre = genrevm,
                    Actor = actorvm,
                    Director = directorvm,
                    Producer = producervm,
                    Studio = studiovm
                };
                return View(vm);

            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Edit(MovieViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Movie movie = _movieRepo.GetSingle(m => m.MovieID == vm.MovieID);
                movie.Title = vm.Title;
                movie.Description = vm.Description;
                movie.Duration = vm.Duration;
                movie.Price = vm.Price;
                movie.Quantity = vm.Quantity;
                movie.ReleaseDate = vm.ReleaseDate;

                _movieRepo.Update(movie);

                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Movie movie = _movieRepo.GetSingle(m => m.MovieID == id);
            MovieGenre movieGenre = _movieGenreRepo.GetSingle(m => m.MovieID == id);
            Genre genre = _genreRepo.GetSingle(g => g.GenreID == movieGenre.GenreID);
            MovieActor movieActor = _movieActorRepo.GetSingle(m => m.MovieID == id);
            Actor actor = _actorRepo.GetSingle(a => a.ActorID == movieActor.ActorID);
            MovieDirector movieDirector = _movieDirectorRepo.GetSingle(m => m.MovieID == id);
            Director director = _directorRepo.GetSingle(d => d.DirectorID == movieDirector.DirectorID);
            MovieProducer movieProducer = _movieProducerRepo.GetSingle(m => m.MovieID == id);
            Producer producer = _producerRepo.GetSingle(p => p.ProducerID == movieProducer.ProducerID);
            MovieStudio movieStudio = _movieStudioRepo.GetSingle(m => m.MovieID == id);
            Studio studio = _studioRepo.GetSingle(s => s.StudioID == movieStudio.StudioID);

            _movieGenreRepo.Delete(movieGenre);
            _movieActorRepo.Delete(movieActor);
            _movieDirectorRepo.Delete(movieDirector);
            _movieProducerRepo.Delete(movieProducer);
            _movieStudioRepo.Delete(movieStudio);

            _movieRepo.Delete(movie);
            _genreRepo.Delete(genre);
            _actorRepo.Delete(actor);
            _directorRepo.Delete(director);
            _producerRepo.Delete(producer);
            _studioRepo.Delete(studio);

            return View("Index");
        }
        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}
