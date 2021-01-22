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
    public class CategoriesController : ControllerBase
    {
        private readonly SkishopContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(SkishopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            List<Category> categories = await _context.Categories.Include(pc => pc.ProductCategories).ToListAsync();
            List<CategoryDTO> categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(categoryDTOs);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            Category found = await _context.Categories.FindAsync(id);

            if (found == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDTO>(found));
        }

        // PUT: api/Categories/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(categoryDTO); //Added after scaffolding to make put method work, otherwise errmsg = The entity type 'CategoryDTO' was not found. Ensure that the entity type has been added to the model...
            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories

        [HttpPost]
        public async Task<ActionResult> PostCategory(CategoryDTO newCategoryDTO)
        {
            Category newCategory = _mapper.Map<Category>(newCategoryDTO);
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCategory), newCategoryDTO);
        }


        // DELETE: api/Categories/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
