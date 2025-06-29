using FunctionFood.Data;
using FunctionFood.Models.Interfaces;

namespace FunctionFood.Models.Services
{
    public class ShopRepository : IShopRepository
    {
        private readonly FunctionalFoodDatabaseContext _context;
        private readonly IShopRepository _shoprepository;
        public ShopRepository(FunctionalFoodDatabaseContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products
                .Where(p => p.Category == category)
                .ToList();
        }
        public void AddProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.ProductId))
            {
                product.ProductId = "SP" + DateTime.Now.ToString("yyyyMMddHHmmss") + Guid.NewGuid().ToString("N")[..4];
            }

            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {

            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public void DeleteProduct(string productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

        }
    }
}
