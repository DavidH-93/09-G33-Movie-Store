using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data;
using MovieStore.Models;
using MovieStore.Services;
using MovieStore.ViewModels;
namespace MovieStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly IMovieRepository _movieRepo;
        private readonly IUserRepository _userRepo;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public OrderController(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo, IMovieRepository movieRepo, IUserRepository userRepo, IGenreRepository genreRepo, IActorRepository actorRepo, IDirectorRepository directorRepo, IProducerRepository producerRepo, IStudioRepository studioRepo, IMovieGenreRepository movieGenreRepo, IMovieActorRepository movieActorRepo, IMovieDirectorRepository movieDirectorRepo, IMovieProducerRepository movieProducerRepo, IMovieStudioRepository movieStudioRepo, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _movieRepo = movieRepo;
            _userRepo = userRepo;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        //GET: Order
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                User u = await _userManager.GetUserAsync(User);
                if(u!= null)
                {
                    Order order = _orderRepo.GetSingle(o => o.UserID.ToString() == u.Id);
                    IEnumerable<OrderItem> orderItems = _orderItemRepo.GetAll();
                    OrderItemViewModel orderItemVm = new OrderItemViewModel();
                    List<OrderItemViewModel> orderItemsVm = new List<OrderItemViewModel>();
                    Movie movie = new Movie();
                    OrderMovieViewModel movieVm = new OrderMovieViewModel();
                    foreach (OrderItem o in orderItems)
                    {
                        movie = _movieRepo.GetSingle(m => m.MovieID == o.MovieID);
                        movieVm.MovieID = movie.MovieID;
                        movieVm.Title = movie.Title;
                        movieVm.Quantity = movie.Quantity;
                        movieVm.Price = movie.Price;
                        orderItemVm.Movie = movieVm;
                        orderItemVm.Price = o.Total();
                        orderItemsVm.Add(orderItemVm);
                    }


                    OrderViewModel orderVm = new OrderViewModel()
                    {
                        OrderID = order.OrderID,
                        UserID = u.Id,
                        Status = order.Status,
                        Creation = order.Creation,
                        Closed = order.Closed
                    };
                    foreach (OrderItemViewModel item in orderItemsVm)
                    {
                        orderVm.OrderItems.Add(item);
                    }
                    return View(orderVm);
                }
            }
            return View("Index");


        }

        //// GET: Order/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(order);
        //}

        //// GET: Order/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Order/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderID,Total,Creation,Closed,Status")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        order.OrderID = Guid.NewGuid();
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(order);
        //}

        //// GET: Order/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Order/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("OrderID,Total,Creation,Closed,Status")] Order order)
        //{
        //    if (id != order.OrderID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.OrderID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(order);
        //}

        //// GET: Order/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order
        //        .FirstOrDefaultAsync(m => m.OrderID == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Order/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var order = await _context.Order.FindAsync(id);
        //    _context.Order.Remove(order);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderExists(Guid id)
        //{
        //    return _context.Order.Any(e => e.OrderID == id);
        //}
    }
}
