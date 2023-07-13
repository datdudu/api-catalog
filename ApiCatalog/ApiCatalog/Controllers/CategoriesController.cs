using ApiCatalog.DTOs;
using ApiCatalog.Models;
using ApiCatalog.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            var category = _context.CategoryRepository.GetCategoriesProducts().ToList();
            var categoryDTO = _mapper.Map<List<CategoryDTO>>(category);

            return categoryDTO;
        }


        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            try
            {
                var category = _context.CategoryRepository.Get().ToList();
                var categoryDTO = _mapper.Map<List<CategoryDTO>>(category);

                return categoryDTO;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            try
            {
                var category = _context.CategoryRepository.GetById(c => c.CategoryId == id);

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
        public ActionResult Post(CategoryDTO categoryDTO) 
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDTO);

                if (category is null)
                    return BadRequest("Invalid Data");

                _context.CategoryRepository.Add(category);
                _context.Commit();

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
        public ActionResult Put(int id, CategoryDTO categoryDTO) 
        {
            try
            {
                if (id != categoryDTO.CategoryId)
                    return BadRequest("Invalid Data");

                var category = _mapper.Map<Category>(categoryDTO);

                _context.CategoryRepository.Update(category);
                _context.Commit();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred trying to process your request");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<CategoryDTO> Delete(int id) 
        {
            try
            {
                var category = _context.CategoryRepository.GetById(c => c.CategoryId == id);

                if (category == null)
                    return NotFound($"Category wth id= {id} Not Found");

                _context.CategoryRepository.Delete(category);
                _context.Commit();

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
