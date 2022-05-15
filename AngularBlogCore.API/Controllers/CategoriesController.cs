using AngularBlogCore.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularBlogCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AngularBlogDBContext _context;

        public CategoriesController(AngularBlogDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            Thread.Sleep(3000); // 3 saniye bekletiyoruz.

            return await _context.Category.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null) return BadRequest();
            return category;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (CategoryExist(id))
            {
                await Task.Run(() => { _context.Category.Update(category); });
                await _context.SaveChangesAsync();
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            if (CategoryExist(id))
            {
                var category = await _context.Category.FindAsync(id);
                _context.Category.Remove(category);
                _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }

        private bool CategoryExist(int id)
        {
            return _context.Category.Any(c => c.Id == id);
        }
    }
}
