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
using Stripe;

namespace MovieStore.Controllers

{
    public class PaymentController : Controller
    {
        private static long PaymentTotal;
        private static Models.Order orderInstance;
        private static IEnumerable<Models.OrderItem> orderItemsInstance;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly IMovieRepository _movieRepo;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public PaymentController(IOrderRepository orderRepo, IOrderItemRepository orderItemRepo, IMovieRepository movieRepo, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
            _movieRepo = movieRepo;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index(Guid id)
        {
            OrderViewModel vm;
            orderInstance = _orderRepo.GetSingle(o => o.OrderID == id);
            orderItemsInstance = _orderItemRepo.Query(i => i.OrderID == orderInstance.OrderID);
            foreach (var item in orderItemsInstance)
            {
                var movie = _movieRepo.GetSingle(o => o.MovieID == item.MovieID);
                if (movie != null)
                {
                    movie.Stock -= item.Quantity;
                    _movieRepo.Update(movie);
                }
            }
            orderInstance.Closed = true;
            orderInstance.Status = "Payment Recieved";
            _orderRepo.Update(orderInstance);

            PaymentTotal = (long)orderInstance.Total * 100;
            vm = new OrderViewModel()
            {
                Total = PaymentTotal,
                OrderID = orderInstance.OrderID
            };
            return View(vm);
        }
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = PaymentTotal,
                Description = "Movie Order Payment",
                Currency = "aud",
                CustomerId = customer.Id
            });


            return View();
        }

    }
}
