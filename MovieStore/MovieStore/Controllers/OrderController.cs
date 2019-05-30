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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly MovieStoreDbContext _context;

        public OrderController(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo, IMovieRepository movieRepo, UserManager<User> userManager, SignInManager<User> signInManager, MovieStoreDbContext context)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _movieRepo = movieRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                User u = await _userManager.GetUserAsync(User);

                Movie movie;
                Order order;
                IEnumerable<OrderItem> items;
                OrderViewModel vm;

                if (_context.Order.Any(o => o.UserID == u.Id && o.Status == "Open"))
                {
                    order = _orderRepo.GetSingle(o => o.UserID == u.Id && o.Status == "Open");
                    items = _orderItemRepo.Query(i => i.OrderID == order.OrderID);
                    if(items.Count() > 0)
                    {
                        vm = new OrderViewModel(order, items);
                        return View(vm);
                    }
                    return RedirectToAction("Empty", "Order");
                }
                return RedirectToAction("Empty", "Order");
            }
            return RedirectToAction("/Account/Login", "Identity");
            }

        public async Task<IActionResult> History()
        {
            if (_signInManager.IsSignedIn(User))
            {
                User u = await _userManager.GetUserAsync(User);
                IEnumerable<Order> orders;
                IEnumerable<OrderItem> items;

                OrderViewModel ovm;
                List<OrderViewModel> ovms = new List<OrderViewModel>();
                List<OrderItemViewModel> ivms = new List<OrderItemViewModel>();

                if (_context.Order.Any(o => o.UserID == u.Id))
                {
                    orders = _orderRepo.Query(o => o.UserID == u.Id);
                    foreach (Order o in orders)
                    {
                        items = _orderItemRepo.Query(i => i.OrderID == o.OrderID);
                        foreach (OrderItem i in items)
                        {
                            ivms.Add(new OrderItemViewModel(i));
                        }
                        ovm = new OrderViewModel(o, items);
                        ovms.Add(ovm);
                    }
                    return View(ovms);
                }
                return RedirectToAction("Empty", "Order");
            }
            return RedirectToAction("/Account/Login", "Identity");
        }
        //this method is horrible because i didnt use the viewmodel paradigm
        public async Task<IActionResult> Add(Guid id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                OrderItem item;
                Order order;
                var u = await _userManager.GetUserAsync(User);

                Movie movie = _movieRepo.GetSingle(m => m.MovieID == id);
                if (_context.Order.Any(o => o.UserID == u.Id && o.Status == "Open"))
                {
                    order = _orderRepo.GetSingle(o => o.UserID == u.Id && o.Status == "Open");
                    IEnumerable<OrderItem> uItems = _orderItemRepo.Query(i => i.OrderID == order.OrderID && i.MovieID == id);
                    if (uItems.Count() > 0)
                    {
                        foreach (OrderItem i in uItems)
                        {
                            i.Quantity += 1;
                            i.setTotal();
                            order.NumItems += 1;
                            order.Total += i.Price;
                            _context.Order.Update(order);
                            //_context.SaveChanges();
                            _context.OrderItem.Update(i);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                            
                        }
                    }
                    item = new OrderItem(u.Id, order.OrderID, id, movie.Title, 1, movie.Price, movie.Price);
                    _orderItemRepo.Create(item);
                    order.Total += item.Total;
                    order.NumItems += 1;
                    _orderRepo.Update(order);
                    return RedirectToAction("Index");
                }
                order = new Order(u.Id, movie.Price);
                item = new OrderItem(u.Id, order.OrderID, id, movie.Title, 1, movie.Price, movie.Price);
                _orderItemRepo.Create(item);
                _orderRepo.Create(order);
                return RedirectToAction("Index");

            }
            return RedirectToAction("/Account/Login", "Identity");
        }


        public IActionResult Remove(Guid id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                OrderItem orderItem = _orderItemRepo.GetSingle(i => i.OrderItemID == id);
                Order order = _orderRepo.GetSingle(o => o.OrderID == orderItem.OrderID);
                order.Total -= orderItem.Total;
                order.NumItems -= orderItem.Quantity;
                _orderItemRepo.Delete(orderItem);
                _orderRepo.Update(order);
                return RedirectToAction("Index");
            }
            return RedirectToAction("/Account/Login", "Identity");
        }

        public IActionResult Cancel(Guid id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                Order order = _orderRepo.GetSingle(o => o.OrderID == id);
                order.Closed = true;
                order.Status = "Cancelled";
                _orderRepo.Update(order);
                return RedirectToAction("Empty", "Order");
            }
            return RedirectToAction("/Account/Login", "Identity");
        }

        public IActionResult Empty()
        {
            return View();
        }
    }
}
