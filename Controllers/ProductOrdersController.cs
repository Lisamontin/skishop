using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace skishop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOrdersController : ControllerBase
    {
        private readonly SkishopContext _context;
        private readonly IMapper _mapper;

        public ProductOrdersController(SkishopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ProductOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOrderDTO>>> GetProductOrders()
        {
            List<ProductOrder> productOrders = await _context.ProductOrders.ToListAsync();
            List<ProductOrderDTO> productOrderDTOs = _mapper.Map<List<ProductOrderDTO>>(productOrders);
            return Ok(productOrderDTOs);
        }

        // GET: api/ProductOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOrderDTO>> GetProductOrder(int id)
        {
            ProductOrder found = await _context.ProductOrders.FindAsync(id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductOrderDTO>(found));
        }

        // PUT: api/ProductOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductOrder(int id, ProductOrderDTO productOrderDTO)
        {
            if (id != productOrderDTO.Id)
            {
                return BadRequest();
            }

            var productOrder = _mapper.Map<ProductOrder>(productOrderDTO);
            _context.Entry(productOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductOrderExists(id))
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

        // POST: api/ProductOrders
        [HttpPost]
        public async Task<ActionResult<ProductOrder>> PostProductOrder(ProductOrderDTO newProductOrderDTO)
        {
            ProductOrder newProductOrder = _mapper.Map<ProductOrder>(newProductOrderDTO);
            _context.ProductOrders.Add(newProductOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostProductOrder), newProductOrderDTO);
        }

        // DELETE: api/ProductOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductOrder(int id)
        {
            var productOrder = await _context.ProductOrders.FindAsync(id);
            if (productOrder == null)
            {
                return NotFound();
            }

            _context.ProductOrders.Remove(productOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductOrderExists(int id)
        {
            return _context.ProductOrders.Any(e => e.Id == id);
        }
    }
}
