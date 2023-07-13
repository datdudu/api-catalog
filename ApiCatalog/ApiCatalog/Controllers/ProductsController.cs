using ApiCatalog.Models;
using ApiCatalog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnityOfWork _context;

        public ProductsController(IUnityOfWork context)
        {
            _context = context;
        }

        [HttpGet("lowerprice")]
        public ActionResult<IEnumerable<Product>> GetProductByPrice()
        {
            return _context.ProductRepository.GetProductsByPrice().ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                var products = _context.ProductRepository.Get().ToList();

                if (products is null)
                    return NotFound("Products Not Found");

                return products;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpGet("{id:int}", Name="GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var product = _context.ProductRepository.GetById(p => p.ProductId == id);

                if (product is null)
                    return NotFound($"Product with id= {id} Not Found");

                return product;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            try
            {
                if (product is null)
                {
                    return BadRequest("Invalid Data");
                }
                _context.ProductRepository.Add(product);
                _context.Commit();

                return new CreatedAtRouteResult("GetProduct",
                        new { id = product.ProductId, product });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            try
            {
                if (id != product.ProductId)
                    return BadRequest("Invalid Data");

                _context.ProductRepository.Update(product);
                _context.Commit();

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _context.ProductRepository.GetById(p => p.ProductId == id);

                if (product is null)
                    return NotFound($"Product with id= {id} Not Found...");

                _context.ProductRepository.Delete(product);
                _context.Commit();

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }
    }
}
