using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FunctionFood.Data;
using FunctionFood.Models.Interfaces;
using FunctionFood.Models.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FunctionFood.Models;

namespace FunctionFood.Controllers
{
    public class AdminController : Controller
    {
        private readonly FunctionalFoodDatabaseContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopRepository _productRepository;
        private readonly IAboutRepository _aboutRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAdminRepository _adminRepository;
        public AdminController(FunctionalFoodDatabaseContext context, IOrderRepository orderRepository, IShopRepository productRepository, IAboutRepository aboutRepository, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IAdminRepository adminRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
            _productRepository = productRepository;

            _aboutRepository = aboutRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _adminRepository = adminRepository;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            var currentYear = DateTime.Now.Year;

            ViewBag.MonthlyProductSold = _adminRepository.GetMonthlyProductSold(currentYear);
            ViewBag.MonthlyRevenue = _adminRepository.GetMonthlyRevenue(currentYear);
            ViewBag.TopProducts = _adminRepository.GetTopProducts(5);
            ViewBag.TotalRevenue = _adminRepository.GetTotalRevenue(currentYear);
            ViewBag.TotalSoldProducts = _adminRepository.GetTotalSoldProducts(currentYear);
            ViewBag.TotalSuccessfulOrders = _adminRepository.GetTotalSuccessfulOrders(currentYear);
            ViewBag.Customers = _adminRepository.GetTotalCustomers();
            ViewBag.Notifications = _adminRepository.GetRecentUnprocessedOrders();

            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageProducts()
        {
            var products = _productRepository.GetAllProducts().ToList();
            ViewBag.TotalProducts = products.Count;
            return View(products);
        }
        [HttpPost]
        public IActionResult DeleteProduct(string id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("ManageProducts");
        }
        [HttpGet]
        public IActionResult EditProduct(string id)
        {
            var product = _productRepository.GetAllProducts().FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(IFormCollection form)
        {
            var product = _productRepository.GetAllProducts()
                            .FirstOrDefault(p => p.ProductId == form["ProductId"]);
            if (product == null) return NotFound();

            product.ProductName = form["ProductName"];
            product.Description = form["Description"];
            product.Category = form["Category"];
            product.Image = form["Image"];
            product.Price = int.TryParse(form["Price"], out var price) ? price : 0;
            product.Trending = form["Trending"] == "true";

            _productRepository.UpdateProduct(product);
            return RedirectToAction("ManageProducts");
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var categories = _context.Products
           .Where(p => !string.IsNullOrEmpty(p.Category))
           .Select(p => p.Category)
           .Distinct()
           .ToList();

            ViewBag.ExistingCategories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(IFormCollection form)
        {
            var product = new Product
            {
                ProductName = form["ProductName"],
                Description = form["Description"],
                Category = form["Category"],
                Image = form["Image"],
                Price = int.TryParse(form["Price"], out var price) ? price : 0,
                Trending = form["Trending"] == "true",

            };

            // Gọi repository để thêm vào database
            _productRepository.AddProduct(product);

            // Điều hướng về trang quản lý
            return RedirectToAction("ManageProducts");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManageUsers()
        {
            var users = _adminRepository.GetAllUsers();
            return View(users);
        }

        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _adminRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _adminRepository.GetUserRolesAsync(user);
            var allRoles = await _adminRepository.GetAllRolesAsync();

            ViewBag.AllRoles = allRoles;
            ViewBag.UserRoles = roles;

            return View(user);
        }

    }
        
        
}
