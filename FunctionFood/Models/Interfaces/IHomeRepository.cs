namespace FunctionFood.Models.Interfaces
{
    public interface IHomeRepository
    {
        IEnumerable<Product> GetTrendingProducts();
    }
}
