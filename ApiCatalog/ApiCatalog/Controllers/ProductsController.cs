using ApiCatalog.DTOs;
using ApiCatalog.Models;
using ApiCatalog.Pagination;
using ApiCatalog.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AllowApiRequest")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnityOfWork _context;
        private readonly IMapper _mapper;

        public ProductsController(IUnityOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("lowerprice")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductByPrice()
        {
            var products = await _context.ProductRepository.GetProductsByPrice();
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get([FromQuery] ProductsParameters productsParameters)
        {
            try
            {
                var products = await _context.ProductRepository.GetProducts(productsParameters);

                if (products is null)
                    return NotFound("Products Not Found");

                var metadata = new
                {
                    products.TotalCount,
                    products.PageSize,
                    products.CurrentPage,
                    products.TotalPages,
                    products.HasNext,
                    products.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                var productsDTO = _mapper.Map<List<ProductDTO>>(products);

                return productsDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpGet("{id:int}", Name="GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            try
            {
                var product = await _context.ProductRepository.GetById(p => p.ProductId == id);

                if (product is null)
                    return NotFound($"Product with id= {id} Not Found");

                var productDTO = _mapper.Map<ProductDTO>(product);

                return productDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            try
            {
                var product = _mapper.Map<Product>(productDTO);

                if (product is null)
                {
                    return BadRequest("Invalid Data");
                }
                _context.ProductRepository.Add(product);
                await _context.Commit();

                var productDto = _mapper.Map<ProductDTO>(product);

                return new CreatedAtRouteResult("GetProduct",
                        new { id = productDto.ProductId, productDto });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ProductDTO productDTO)
        {
            try
            {
                if (id != productDTO.ProductId)
                    return BadRequest("Invalid Data");

                var product = _mapper.Map<Product>(productDTO);

                _context.ProductRepository.Update(product);
                await _context.Commit();

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            try
            {
                var product = await _context.ProductRepository.GetById(p => p.ProductId == id);

                if (product is null)
                    return NotFound($"Product with id= {id} Not Found...");

                _context.ProductRepository.Delete(product);
                await _context.Commit();

                var productDTO = _mapper.Map<ProductDTO>(product);

                return Ok(productDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }
    }
}
