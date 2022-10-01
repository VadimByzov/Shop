using Dapper;
using Microsoft.Data.Sqlite;
using Shop.DataAccess.Models;

namespace Shop.DataAccess.Services;

public class ProductDataService : IProductDataService
{
  private readonly string _connectionString;

  public ProductDataService(IConfiguration configuration)
  {
    _connectionString = configuration.GetConnectionString("Default");
  }

  public async Task<DataProduct> CreateAsync(DataProduct product)
  {
    var query = "INSERT INTO Product (Name, Description, Price) VALUES " +
      "(@Name, @Description, @Price) RETURNING *";
    using var connection = new SqliteConnection(_connectionString);
    var result = await connection.QueryAsync<DataProduct>(query,
      new { product.Name, product.Description, product.Price });

    return result.Single();
  }

  public async Task<DataProduct?> ReadByIdAsync(int id)
  {
    var query = "SELECT * FROM Product WHERE Id=@id";
    using var connection = new SqliteConnection(_connectionString);
    var result = await connection.QueryAsync<DataProduct>(query, new { id });

    return result.SingleOrDefault();
  }

  public async Task<IEnumerable<DataProduct>> ReadAllAsync()
  {
    var query = "SELECT * FROM Product";
    using var connection = new SqliteConnection(_connectionString);
    var result = await connection.QueryAsync<DataProduct>(query);

    return result.Select(x => new DataProduct(x.Id, x.Name, x.Description, x.Price)).ToList();
  }

  public async Task UpdateAsync(DataProduct product)
  {
    var query = "UPDATE Product SET Name=@Name, Description=@Description, " +
      "Price=@Price WHERE Id=@Id";
    using var connection = new SqliteConnection(_connectionString);
    await connection.ExecuteAsync(query, product);
  }

  public async Task DeleteByIdAsync(int id)
  {
    var query = "DELETE FROM Product WHERE Id=@id";
    using var connection = new SqliteConnection(_connectionString);
    await connection.ExecuteAsync(query, new { id });
  }

  private async Task ExecuteAsync(string query, object param)
  {
    using var connection = new SqliteConnection(_connectionString);
    await connection.ExecuteAsync(query, param);
  }

  private async Task<IEnumerable<DataProduct>?> QueryAsync(string query, object? param)
  {
    using var connection = new SqliteConnection(_connectionString);

    return await connection.QueryAsync<DataProduct>(query, param);
  }
}
