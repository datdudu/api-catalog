using ApiCatalog.Context;
using ApiCatalog.Models;
using ApiCatalog.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnityOfWork _context;

        public CategoriesController(IUnityOfWork context)
        {
            _context = context;
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            return _context.CategoryRepository.GetCategoriesProducts().ToList();
        }


        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                return _context.CategoryRepository.Get().ToList();
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
                var category = _context.CategoryRepository.GetById(c => c.CategoryId == id);

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

                _context.CategoryRepository.Add(category);
                _context.Commit();

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

                _context.CategoryRepository.Update(category);
                _context.Commit();

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
                var category = _context.CategoryRepository.GetById(c => c.CategoryId == id);

                if (category == null)
                    return NotFound($"Category wth id= {id} Not Found");

                _context.CategoryRepository.Delete(category);
                _context.Commit();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }
    }
}
