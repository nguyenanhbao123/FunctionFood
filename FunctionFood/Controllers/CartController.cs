using FunctionFood.Models.Interfaces;
using FunctionFood.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FunctionFood.Controllers
{
  
        public class CartController : Controller
        {
            private readonly ICartRepository _cartService;

            public CartController(ICartRepository cartService)
            {
                _cartService = cartService;
            }

            // 1. Hiển thị giỏ hàng
            public async Task<IActionResult> CartIndex()
            {
                var cart = await _cartService.GetCartAsync(); // Tự động lấy từ session
                return View("CartIndex", cart);
        }
            [HttpGet]
            public async Task<IActionResult> CartPopup()
            {
                return ViewComponent("Cart");
            }

            // 2. Thêm sản phẩm vào giỏ hàng
            [HttpPost]
            public async Task<IActionResult> AddToCart(string productId, int quantity = 1)
            {
                await _cartService.AddItemToCartAsync(productId, quantity);
                var cart = await _cartService.GetCartAsync();
                return Json(new { success = true });
            }

            // 3. Cập nhật số lượng sản phẩm
            [HttpPost]
            public async Task<IActionResult> UpdateQuantity(string cartItemId, int quantity)
            {
                await _cartService.UpdateItemQuantityAsync(cartItemId, quantity);
                return RedirectToAction("CartIndex");
            }

            // 4. Xoá một sản phẩm khỏi giỏ
            [HttpPost]
            public async Task<IActionResult> RemoveItem(string cartItemId)
            {
                await _cartService.RemoveItemFromCartAsync(cartItemId);
                var cart = await _cartService.GetCartAsync();
                return Json(new { success = true });
            }

            // 5. Xoá toàn bộ giỏ hàng
            [HttpPost]
            public async Task<IActionResult> ClearCart()
            {
                await _cartService.ClearCartAsync();
                return RedirectToAction("CartIndex");
            }

        // 6. Thanh toán
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var cart = await _cartService.GetCartAsync();
                ViewBag.CheckoutError = "Vui lòng điền đầy đủ thông tin người nhận.";
                return View("CartIndex", cart); // Sử dụng CartIndex.cshtml
            }

            string? userId = User.Identity != null && User.Identity.IsAuthenticated
                ? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                : null;

            await _cartService.CheckoutAsync(userId, model);
            TempData["Success"] = "Đơn hàng của bạn đã được ghi nhận!";
            return RedirectToAction("CartIndex");
        }


    }
    
}
