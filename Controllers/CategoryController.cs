using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }


        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromRoute] BlogDataContext context)
        {
            var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }


        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel model,
            [FromRoute] BlogDataContext context)
        {
            try
            {
                var category = new Category
                {
                    Id = 0,
                    Posts = null,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, "05XE9 - Não foi possível incluir a categoria.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X10 - Falha interna no servidor.");
            }

        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModel model,
            [FromRoute] BlogDataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE8 - Não foi possível alterar a categoria.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X07 - Falha interna no servidor.");
            }
        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromRoute] BlogDataContext context)
        {
            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05X11 - Não foi possível excluir a categoria.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05X12 - Falha interna no servidor.");
            }
        }
    }
}
