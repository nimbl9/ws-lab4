using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetAsync(int id);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}