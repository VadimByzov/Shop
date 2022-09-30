using Shop.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices();
builder.Services.AddShopServices();

var app = builder.Build();

app.InitializationDatabase();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
});

app.Run();
