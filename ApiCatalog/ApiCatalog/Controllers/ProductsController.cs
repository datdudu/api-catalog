using ApiCatalog.Context;
using ApiCatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiCatalogContext _context;

        public ProductsController(ApiCatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _context.Products.ToList();

            if (products is null)
                return NotFound("Products Not Found");

            return products;
        }

        [HttpGet("{id:int}", Name="GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if(product is null)
                return NotFound("Product Not Found");
            
            return product;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct",
                    new { id = product.ProductId, product });
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
                return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product is null)
                return NotFound("Product Not Found...");

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
