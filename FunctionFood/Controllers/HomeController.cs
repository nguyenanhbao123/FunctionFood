using Microsoft.AspNetCore.Mvc;
using FunctionFood.Data;
using FunctionFood.Models.Interfaces;
namespace FunctionFood.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly FunctionalFoodDatabaseContext _context;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, FunctionalFoodDatabaseContext context, IHomeRepository homeRepository, ICartRepository cartRepository)
        {
            _logger = logger;
            _context = context;
            _homeRepository = homeRepository;
            _cartRepository = cartRepository;
        }
        public IActionResult HomeIndex()
        {
            var trendingProducts = _homeRepository.GetTrendingProducts();
            return View(trendingProducts);
        }
    }
}
