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
    public class ProductCategoriesController : ControllerBase
    {
        private readonly SkishopContext _context;
        private readonly IMapper _mapper;

        public ProductCategoriesController(SkishopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategories()
        {
            List<ProductCategory> productCategories = await _context.ProductCategories.ToListAsync();
            List<ProductCategoryDTO> productCategoryDTOs = _mapper.Map<List<ProductCategoryDTO>>(productCategories);
            return Ok(productCategoryDTOs);
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDTO>> GetProductCategory(int id)
        {
            ProductCategory found = await _context.ProductCategories.FindAsync(id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductCategoryDTO>(found));
        }

        // PUT: api/ProductCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategoryDTO productCategoryDTO)
        {
            if (id != productCategoryDTO.Id)
            {
                return BadRequest();
            }

            var productCategory = _mapper.Map<ProductCategory>(productCategoryDTO);
            _context.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST: api/ProductCategories
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategoryDTO newProductCategoryDTO)
        {
            ProductCategory newProductCategory = _mapper.Map<ProductCategory>(newProductCategoryDTO);
            _context.ProductCategories.Add(newProductCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductCategory), newProductCategoryDTO);
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCategoryExists(int id)
        {
            return _context.ProductCategories.Any(e => e.Id == id);
        }
    }
}
