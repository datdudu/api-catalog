using ApiCatalog.DTOs;
using ApiCatalog.Models;
using ApiCatalog.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
        public ActionResult<IEnumerable<ProductDTO>> GetProductByPrice()
        {
            var products = _context.ProductRepository.GetProductsByPrice().ToList();
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            try
            {
                var products = _context.ProductRepository.Get().ToList();

                if (products is null)
                    return NotFound("Products Not Found");

                var productsDTO = _mapper.Map<List<ProductDTO>>(products);

                return productsDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpGet("{id:int}", Name="GetProduct")]
        public ActionResult<ProductDTO> Get(int id)
        {
            try
            {
                var product = _context.ProductRepository.GetById(p => p.ProductId == id);

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
        public ActionResult Post([FromBody] ProductDTO productDTO)
        {
            try
            {
                var product = _mapper.Map<Product>(productDTO);

                if (product is null)
                {
                    return BadRequest("Invalid Data");
                }
                _context.ProductRepository.Add(product);
                _context.Commit();

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
        public ActionResult Put(int id, ProductDTO productDTO)
        {
            try
            {
                if (id != productDTO.ProductId)
                    return BadRequest("Invalid Data");

                var product = _mapper.Map<Product>(productDTO);

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
        public ActionResult<ProductDTO> Delete(int id)
        {
            try
            {
                var product = _context.ProductRepository.GetById(p => p.ProductId == id);

                if (product is null)
                    return NotFound($"Product with id= {id} Not Found...");

                _context.ProductRepository.Delete(product);
                _context.Commit();

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
