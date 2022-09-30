using Shop.DataAccess.Models;

namespace Shop.DataAccess.Services;

public interface IProductDataService
{
  Task<DataProduct> CreateAsync(DataProduct product);

  Task<DataProduct?> ReadByIdAsync(int id);

  Task<IEnumerable<DataProduct>> ReadAllAsync();

  Task UpdateAsync(DataProduct product);

  Task DeleteByIdAsync(int id);
}
