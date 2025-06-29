using FunctionFood.Data;
using FunctionFood.Models.Interfaces;

namespace FunctionFood.Models.Services
{
    public class HomeRepository : IHomeRepository
    {
        private readonly FunctionalFoodDatabaseContext _context;
        public HomeRepository(FunctionalFoodDatabaseContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetTrendingProducts()
        {
            return _context.Products
                .Where(p => p.Trending == true)
                .OrderBy(p => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
                .Take(3)
                .ToList();
        }
    }
}
