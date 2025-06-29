using Microsoft.AspNetCore.Identity;

namespace FunctionFood.Models.Interfaces
{
    public interface IAdminRepository
    {
        Dictionary<int, int> GetMonthlyProductSold(int year);
        Dictionary<int, decimal> GetMonthlyRevenue(int year);
        List<ProductTotal> GetTopProducts(int top);

        decimal GetTotalRevenue(int year);
        int GetTotalSoldProducts(int year);
        int GetTotalSuccessfulOrders(int year);
        int GetTotalCustomers();

        List<IdentityUser> GetAllUsers();
        Task<IdentityUser?> GetUserByIdAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(IdentityUser user);
        Task UpdateUserRolesAsync(IdentityUser user, IEnumerable<string> roles);
        Task<List<string>> GetAllRolesAsync();

        List<OrderNotification> GetRecentUnprocessedOrders(int limit = 5);
    }
}
