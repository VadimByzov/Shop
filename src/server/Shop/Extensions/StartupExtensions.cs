using Shop.DataAccess.Services;
using Shop.Services;

namespace Shop.Extensions
{
  public static class StartupExtensions
  {
    public static void ConfigureServices(this IServiceCollection services)
    {
      services.AddControllers();
    }

    public static void AddShopServices(this IServiceCollection services)
    {
      services.AddTransient<IDatabaseInitializationService, SQLiteInitializationService>();
      services.AddTransient<IProductDataService, ProductDataService>();
      services.AddTransient<IProductService, ProductService>();
    }

    public static void InitializationDatabase(this IApplicationBuilder application)
    {
      application.ApplicationServices
        .GetService<IDatabaseInitializationService>()?
        .Initialization().Wait();
    }
  }
}
