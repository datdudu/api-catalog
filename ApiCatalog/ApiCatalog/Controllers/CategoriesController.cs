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
    public class CategoriesController : ControllerBase
    {
        private readonly IUnityOfWork _context;
        private readonly IMapper _mapper;

        public CategoriesController(IUnityOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var category = await _context.CategoryRepository.GetCategoriesProducts();
            var categoryDTO = _mapper.Map<List<CategoryDTO>>(category);

            return categoryDTO;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get([FromQuery] CategoriesParameters categoriesParameters)
        {
            try
            {
                var categories = await _context.CategoryRepository.GetCategories(categoriesParameters);


                var metadata = new
                {
                    categories.TotalCount,
                    categories.PageSize,
                    categories.CurrentPage,
                    categories.TotalPages,
                    categories.HasNext,
                    categories.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var categoryDTO = _mapper.Map<List<CategoryDTO>>(categories);

                return categoryDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            try
            {
                var category = await _context.CategoryRepository.GetById(c => c.CategoryId == id);

                if (category == null)
                {
                    return NotFound($"Category with id= {id} Not Found");
                }

                var categoryDTO = _mapper.Map<CategoryDTO>(category);

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoryDTO categoryDTO) 
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);

                if (category is null)
                    return BadRequest("Invalid Data");

                _context.CategoryRepository.Add(category);
                await _context.Commit();

                var categoryDto = _mapper.Map<CategoryDTO>(category);

                return new CreatedAtRouteResult("GetCategory",
                    new { id = categoryDto.CategoryId }, categoryDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpPut("{id}")]
        public async  Task<ActionResult> Put(int id, CategoryDTO categoryDTO) 
        {
            try
            {
                if (id != categoryDTO.CategoryId)
                    return BadRequest("Invalid Data");

                var category = _mapper.Map<Category>(categoryDTO);

                _context.CategoryRepository.Update(category);
                await _context.Commit();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id) 
        {
            try
            {
                var category = await _context.CategoryRepository.GetById(c => c.CategoryId == id);

                if (category == null)
                    return NotFound($"Category wth id= {id} Not Found");

                _context.CategoryRepository.Delete(category);
                await _context.Commit();

                var categoryDTO = _mapper.Map<CategoryDTO>(category);

                return Ok(categoryDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }
    }
}
