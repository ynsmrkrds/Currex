using Currex.Enums.TCMBFinancialRate;
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
            ViewBag.RefreshTime = FindRefreshTime();

            FinancialMarketRateModel marketRateModel = _financialMarketRateManager.GetCurrentAsync().Result;
            return View(marketRateModel);
        }

        private static DateTime FindRefreshTime()
        {
            var now = DateTime.Now;

            var validHours = Enum.GetValues<TCMBFinancialRateHourlyValidHours>()
                .Select(e => (int)e)
                .ToList();

            var todayValidTimes = validHours
                .Select(h => new DateTime(now.Year, now.Month, now.Day, h / 100, 0, 0))
                .OrderBy(t => t)
                .ToList();

            var nextValidTime = todayValidTimes.FirstOrDefault(t => t >= now);

            if (nextValidTime == default)
            {
                var tomorrow = now.AddDays(1);
                nextValidTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, validHours.Min() / 100, 0, 0);
            }

            return nextValidTime.AddMinutes(5);
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
