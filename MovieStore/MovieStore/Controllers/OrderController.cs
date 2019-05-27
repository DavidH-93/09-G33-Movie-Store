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
        private readonly MovieStoreDbContext _context;

        public OrderController(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo, IMovieRepository movieRepo, IUserRepository userRepo, IGenreRepository genreRepo, IActorRepository actorRepo, IDirectorRepository directorRepo, IProducerRepository producerRepo, IStudioRepository studioRepo, IMovieGenreRepository movieGenreRepo, IMovieActorRepository movieActorRepo, IMovieDirectorRepository movieDirectorRepo, IMovieProducerRepository movieProducerRepo, IMovieStudioRepository movieStudioRepo, UserManager<User> userManager, SignInManager<User> signInManager, MovieStoreDbContext context)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _movieRepo = movieRepo;
            _userRepo = userRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Order userOrder;
            IEnumerable<OrderItem> userOrderItems;
            MovieOrderItemViewModel movieViewModel;
            OrderViewModel orderViewModel;
            OrderItemViewModel orderItemViewModel = new OrderItemViewModel();
            List<OrderItemViewModel> orderItemViewModels = new List<OrderItemViewModel>();
            Movie movie = new Movie();
            if (_signInManager.IsSignedIn(User))
            {
                User u = await _userManager.GetUserAsync(User);
                if (_context.Order.Any(o => o.UserID == u.Id))
                {
                    userOrder = _orderRepo.GetSingle(o => o.UserID == u.Id && o.Closed == false);
                    userOrderItems = _orderItemRepo.Query(o => o.OrderID == userOrder.OrderID);
                    orderViewModel = new OrderViewModel()
                    {
                        Creation = userOrder.Creation,
                        OrderID = userOrder.OrderID,
                        Status = userOrder.Status,
                        Total = userOrder.Total,
                        UserID = userOrder.UserID
                    };
                    foreach (OrderItem o in userOrderItems)
                    {
                        movie = _movieRepo.GetSingle(m => m.MovieID == o.MovieID);
                        movieViewModel = new MovieOrderItemViewModel()
                        {
                            MovieID = movie.MovieID,
                            Title = movie.Title
                        };
                        orderItemViewModel.OrderItemID = o.OrderItemID;
                        orderItemViewModel.Price = o.Price;
                        orderItemViewModel.Total = o.Total();
                        orderItemViewModel.Quantity = o.Amount;
                        orderItemViewModel.Movie = movieViewModel;
                        orderItemViewModels.Add(orderItemViewModel);
                    };
                    orderViewModel.OrderItems = orderItemViewModels;
                    return View(orderViewModel);
                }
                return RedirectToAction("Empty", "Order");
            }
            return RedirectToAction("/Account/Login", "Identity");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid id, MovieViewModel vm)
        {
            Order userOrder;
            Order newOrder;
            Movie movie;
            OrderItem newOrderItem;
            //OrderOrderItem newOrderOrderItem;
            List<OrderItem> orderItems = new List<OrderItem>();
            IEnumerable<OrderItem> userOrderItems = new List<OrderItem>();
            //IEnumerable<OrderOrderItem> userOrderOrderItems;
            if (_signInManager.IsSignedIn(User))
            {
                //ADD ITEM TO ORDER

                //get user
                User u = await _userManager.GetUserAsync(User);
                movie = _movieRepo.GetSingle(m => m.MovieID == id);
                //check if user has an existing order item for the same item
                if (_context.Order.Any(o => o.UserID == u.Id))
                 {
                    //get open user order
                    userOrder = _orderRepo.GetSingle(o => o.UserID == u.Id && o.Closed == false);


                        //populate list of order items
                        userOrderItems = _orderItemRepo.Query(o => o.OrderID == userOrder.OrderID);
                        //test list for matching existing items
                        foreach (OrderItem item in userOrderItems)
                        {
                            if (item.MovieID == movie.MovieID)
                            {
                                //add to existing order item
                                item.Amount += vm.Amount;
                                _orderItemRepo.Update(item);
                                userOrder.Total = userOrder.CalculateTotal(userOrderItems);

                                _orderItemRepo.Update(item);
                                _orderRepo.Update(userOrder);
                                return RedirectToAction("ReadAll", "Movie");
                            }
                        }
                        //create new order item
                        newOrderItem = new OrderItem()
                        {
                            OrderItemID = new Guid(),
                            OrderID = userOrder.OrderID,
                            UserID = u.Id,
                            MovieID = movie.MovieID,
                            Price = movie.Price,
                            Amount = vm.Amount
                        };

                        userOrderItems.Append<OrderItem>(newOrderItem);
                        userOrder.Total = userOrder.CalculateTotal(userOrderItems);

                        //create new order item to existing user order
                        _orderItemRepo.Create(newOrderItem);
                        _orderRepo.Update(userOrder);
                        return RedirectToAction("ReadAll", "Movie");     
                }
                else
                {
                    newOrder = new Order()
                    {
                        OrderID = new Guid(),
                        UserID = u.Id,
                        Closed = false,
                        Creation = DateTime.Now,
                        Status = "Open"
                    };
                    newOrderItem = new OrderItem()
                    {
                        OrderItemID = new Guid(),
                        OrderID = newOrder.OrderID,
                        UserID = u.Id,
                        MovieID = movie.MovieID,
                        Price = movie.Price,
                        Amount = vm.Amount
                    };
                    newOrder.Total = newOrderItem.Price * newOrderItem.Amount;

                    _orderItemRepo.Create(newOrderItem);
                    _orderRepo.Create(newOrder);
                    return RedirectToAction("ReadAll", "Movie");
                }
            }
            return RedirectToAction("/Account/Login", "Identity");
        }

        public async Task<IActionResult> Remove(Guid id)
        {
            if (_signInManager.IsSignedIn(User))
            {

            }
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

    }
}
