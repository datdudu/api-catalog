using ApiCatalog.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
