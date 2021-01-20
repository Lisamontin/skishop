using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace skishop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SkishopContext _context;
        private readonly IMapper _mapper;

        public OrdersController(SkishopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            List<Order> orders = await _context.Orders.Include(po => po.ProductOrders).ToListAsync();
            List<OrderDTO> orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
            return Ok(orderDTOs);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            Order found = await _context.Orders.FindAsync(id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderDTO>(found));
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
            {
                return BadRequest();
            }

            var order = _mapper.Map<Order>(orderDTO);
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDTO newOrderDTO)
        {
            Order newOrder = _mapper.Map<Order>(newOrderDTO);
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), newOrder);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
