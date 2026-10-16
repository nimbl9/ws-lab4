using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<Product?> GetAsync(int id)
    {
        return await _db.Products.FindAsync(id);
    }

    public async Task CreateAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _db.Products.FindAsync(id);

        if (product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
    }
}