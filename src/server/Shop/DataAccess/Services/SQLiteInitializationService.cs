using Dapper;
using Microsoft.Data.Sqlite;

namespace Shop.DataAccess.Services;

public class SQLiteInitializationService : IDatabaseInitializationService
{
  private readonly string _connectionString;

  public SQLiteInitializationService(IConfiguration configuration)
  {
    _connectionString = configuration.GetConnectionString("Default");
  }

  private const string CreateProductTableIfNotExists = "CREATE TABLE IF NOT EXISTS Product " +
    "(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Description TEXT, Price REAL)";

  private const string InsertTestData = "INSERT INTO Product (Name, Description, Price) VALUES " +
    "('Water 0.5','Just water in a 0.5L bottle',29.99)," +
    "('Bread','Tasty bread',24.99)," +
    "('Analgin','Helps with head hurt', 79.99)";

  public async Task Initialization()
  {
    using var connection = new SqliteConnection(_connectionString);
    await connection.ExecuteAsync(CreateProductTableIfNotExists);

    var count = await connection.QueryAsync<int>("SELECT COUNT(*) FROM Product");
    if (count.Single() == 0)
    {
      await connection.ExecuteAsync(InsertTestData);
    }
  }
}
