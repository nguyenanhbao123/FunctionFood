using Microsoft.AspNetCore.Mvc;
using FunctionFood.Data;
using FunctionFood.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FunctionFood.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutRepository aboutRepository;
        private readonly FunctionalFoodDatabaseContext functionalfoodDatabaseContext;
        public AboutController(IAboutRepository aboutRepository, FunctionalFoodDatabaseContext gorocoDatabaseContext)
        {
            this.aboutRepository = aboutRepository;
            this.functionalfoodDatabaseContext = gorocoDatabaseContext;
        }

        public IActionResult AboutIndex()
        {
            var galleries = aboutRepository.GetGallery();
            return View(galleries);
        }
    }
}
