using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Models;
using Stripe;

namespace MovieStore.Controllers
{
    public class HomeController : Controller
    {


        //httpget
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Charge(string StripeEmail, string StripeToken)
        //{
        //    var customerService = new StripeCustomerService();
        //    var ChargeService = new StripeChargeService();

        //    var customer = new CustomerService.Create(new StripeCustomerCreateOptions
        //    {
        //        Email = StripeEmail,
        //        SourceToken = StripeToken
        //    });

        //    var charge = chargeService.Create(new StripeChargeCreateOptions
        //    {
        //        Amount = 500,
        //        Description = "Movie Store Test",
        //        Currency = "aud",
        //        CustomerId = customer.Id
        //    });

        //    return View();

        //}
    }
}
