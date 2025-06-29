using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FunctionFood.Data;
using FunctionFood.Models.Interfaces;

namespace FunctionFood.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly FunctionalFoodDatabaseContext _context;
        private readonly IFavoriteProducts favoriteProducts;
        public FavoriteController(FunctionalFoodDatabaseContext context, IFavoriteProducts favoriteProducts)
        {
            _context = context;
            this.favoriteProducts = favoriteProducts;
        }
        public IActionResult FavoriteIndex()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToAction("Login", "Account");
            var favoriteProductsList = favoriteProducts.GetFavoriteProducts(userId);
            return View(favoriteProductsList);
        }
        [HttpGet]
        public IActionResult AddToFavorite(string productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                // Trả về JSON thay vì redirect
                return Json(new { redirectUrl = Url.Action("Login", "Account") });
            }

            var updatedFavorites = favoriteProducts.AddFavoriteProducts(userId, productId);
            return Json(new { success = true });
        }

        public IActionResult RemoveFromFavorite(string productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Json(new { redirectUrl = Url.Action("Login", "Account") });
            }

            var updatedFavorites = favoriteProducts.RemoveFavoriteProducts(userId, productId);
            return Ok();
        }
    }
}
