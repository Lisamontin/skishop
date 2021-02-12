using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Products.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly SkishopContext _context;
    private readonly IMapper _mapper;
    public ProductsController(SkishopContext context, IMapper mapper) 
    {
      _context = context;
      _mapper = mapper;
    }

///////////////////////////// GET //////////////////////////////

    [HttpGet]
    public async Task<ActionResult> GetProducts() 
    {
      List<Product> products = await _context.Products
        .Include(po => po.ProductOrders)
        .Include(pc => pc.ProductCategories).ToListAsync();

      List<ProductDTO> productDTOs = _mapper.Map<List<ProductDTO>>(products);

      return Ok(productDTOs);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetById(int id) 
    {
      Product found = await _context.Products.FindAsync(id);
      // Product found = await _context.Products.Include(po => po.ProductOrders).Include(pc => pc.ProductCategories).FirstAsync(po => po.Id == id)  //.FirstAsync(pc => pc.Id ==id);

      if(found == null) {
        return NotFound();
      }

      return Ok(_mapper.Map<ProductDTO>(found));
    }

//////////////////////////// POST //////////////////////////////

    [HttpPost]
    public async Task<ActionResult> CreateProduct(ProductDTO newProductDTO)
    {
      Product newProduct = _mapper.Map<Product>(newProductDTO);

      _context.Products.Add(newProduct);
      await _context.SaveChangesAsync();

      return CreatedAtAction("CreateProduct", newProductDTO);
    }
    
//////////////////////////// PUT //////////////////////////////

    [HttpPut("{id}")]
      public async Task<IActionResult> PutProduct(int id, ProductDTO productDTO)
    {
      if (id != productDTO.Id)
      {
          return BadRequest();
      }

      var product = _mapper.Map<Product>(productDTO);
      _context.Entry(product).State = EntityState.Modified;

      try
      {
          await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
          if (!ProductExists(id))
          {
              return NotFound();
          }
          else
          {
              throw;
          }
      }

      return NoContent();
    }

//////////////////////////// DELETE //////////////////////////////

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
  private bool ProductExists(int id)
  {
      return _context.Products.Any(e => e.Id == id);
  }
  }

}
