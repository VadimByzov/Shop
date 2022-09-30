using Shop.Models;

namespace Shop.Services;

public interface IProductService
{
  Task<Product> CreateAsync(Product product);

  Task<Product?> ReadByIdAsync(int id);

  Task<IEnumerable<Product>> ReadAllAsync();

  Task UpdateAsync(Product product);

  Task DeleteByIdAsync(int id);
}
