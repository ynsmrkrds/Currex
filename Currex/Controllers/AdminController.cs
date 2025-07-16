using Currex.Models.Admin;
using Currex.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Currex.Controllers
{
    public class AdminController(IFinancialAssetMarginService marginService) : Controller
    {
        public IActionResult Index()
        {
            var models = marginService.GetAll();
            return View(models);
        }

        [HttpPost]
        public IActionResult UpdateMargins(List<FinancialAssetMarginModel> models)
        {
            try
            {
                marginService.UpdateAll(models);
                ViewBag.Message = "Veriler başarıyla güncellendi.";
            }
            catch (Exception)
            {
                ViewBag.Message = "Veriler güncellenirken bir hata oluştu.";
            }

            return View(models);
        }

    }
}
