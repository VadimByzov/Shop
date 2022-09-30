using Microsoft.AspNetCore.Mvc;
using Shop.Exceptions;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
  private readonly IProductService _productService;

  public ProductController(IProductService productService)
  {
    _productService = productService;
  }

  [HttpPost("")]
  public async Task<ActionResult<Product>> Post(Product product)
  {
    try
    {
      var created = await _productService.CreateAsync(product);

      return CreatedAtAction(nameof(GetById), created.Id, created);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("")]
  public async Task<ActionResult<IEnumerable<Product>>> Get()
  {
    try
    {
      var result = await _productService.ReadAllAsync();

      return Ok(result);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Product>> GetById(int id)
  {
    try
    {
      var result = await _productService.ReadByIdAsync(id);

      return Ok(result);
    }
    catch (NotFoundException e)
    {
      return NotFound(e.Message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> PutById(int id, Product product)
  {
    if (id != product.Id)
    {
      return BadRequest("ids are not equals!");
    }

    try
    {
      await _productService.UpdateAsync(product);

      return NoContent();
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteById(int id)
  {
    try
    {
      await _productService.DeleteByIdAsync(id);

      return NoContent();
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
