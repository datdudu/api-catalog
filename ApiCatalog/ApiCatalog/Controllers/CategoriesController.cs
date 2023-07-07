using ApiCatalog.Context;
using ApiCatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiCatalogContext _context;

        public CategoriesController(ApiCatalogContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetProductsByCategory()
        {
            try
            {
                return _context.Categories.Include(p => p.Products).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                return _context.Categories.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

                if (category == null)
                {
                    return NotFound($"Category with id= {id} Not Found");
                }

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpPost]
        public ActionResult Post(Category category) 
        {
            try
            {
                if (category is null)
                    return BadRequest("Invalid Data");

                _context.Categories.Add(category);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.CategoryId }, category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpPut]
        public ActionResult Put(int id, Category category) 
        {
            try
            {
                if (id != category.CategoryId)
                    return BadRequest("Invalid Data");

                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id) 
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

                if (category == null)
                    return NotFound($"Category wth id= {id} Not Found");

                _context.Categories.Remove(category);
                _context.SaveChanges();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }
    }
}
