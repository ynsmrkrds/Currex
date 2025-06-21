using Currex.Managers;
using Currex.Models;
using Currex.Models.FinancialMarketRate;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Currex.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private FinancialMarketRateManager _financialMarketRateManager;

        public HomeController(ILogger<HomeController> logger, FinancialMarketRateManager financialMarketRateManager)
        {
            _logger = logger;
            _financialMarketRateManager = financialMarketRateManager;
        }

        public IActionResult Index()
        {
            FinancialMarketRateModel a = _financialMarketRateManager.GetCurrentAsync().Result;
            return View();
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
