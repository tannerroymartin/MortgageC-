using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mortgage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Mortgage.LoanHelpers;

namespace Mortgage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult App()
        {
            Loan loan = new Loan();
            return View(loan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult App(Loan loan)
        {
            var LoanHelpers = new LoanUtils();
            var RemainingBalance = loan.Amount;
            

            var MonthlyPayment = LoanHelpers.CalcPayment(loan.Amount, loan.Rate, loan.Term);
            var TotalInterest = 0.00m;
            var MonthlyInterest = 0.00m;


            for (int i = 1; i <= loan.Term; i++)
            {
                var loanPayment = new LoanPayment();
                
                loanPayment.Month = i;
                MonthlyInterest = LoanHelpers.CalcMonthlyInterest(RemainingBalance, loan.Rate);
                loanPayment.Payment = MonthlyPayment;
                loanPayment.MonthlyInterest = MonthlyInterest;
                loanPayment.MonthlyPrinciapl = MonthlyPayment - MonthlyInterest;
                var MonthlyPrincipal = MonthlyPayment - MonthlyInterest;
                TotalInterest = MonthlyInterest + TotalInterest;
                loanPayment.TotalInterest = TotalInterest;
                RemainingBalance = RemainingBalance - MonthlyPrincipal;
                loanPayment.Balance = RemainingBalance;


                loan.Payments.Add(loanPayment);

            }

            return View(loan);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
