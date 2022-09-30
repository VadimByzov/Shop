using Shop.DataAccess.Models;
using Shop.DataAccess.Services;
using Shop.Exceptions;
using Shop.Models;

namespace Shop.Services;

public class ProductService : IProductService
{
  private readonly IProductDataService _productDataService;

  public ProductService(IProductDataService productDataService)
  {
    _productDataService = productDataService;
  }

  public async Task<Product> CreateAsync(Product product)
  {
    var data = await _productDataService.CreateAsync(
      new DataProduct(product.Id, product.Name, product.Description, product.Price));

    return new Product(data.Id, data.Name, data.Description, data.Price);
  }

  public async Task<Product?> ReadByIdAsync(int id)
  {
    var data = await _productDataService.ReadByIdAsync(id);

    if (data == null)
      throw new NotFoundException("Product not found!");

    return new Product(data.Id, data.Name, data.Description, data.Price);
  }

  public async Task<IEnumerable<Product>> ReadAllAsync()
  {
    var collection = await _productDataService.ReadAllAsync();

    return collection.Select(x => new Product(x.Id, x.Name, x.Description, x.Price));
  }

  public async Task UpdateAsync(Product product)
  {
    await _productDataService.UpdateAsync(
      new DataProduct(product.Id, product.Name, product.Description, product.Price));
  }

  public async Task DeleteByIdAsync(int id)
  {
    await _productDataService.DeleteByIdAsync(id);
  }
}
