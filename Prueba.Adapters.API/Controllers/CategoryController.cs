using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prueba.Domain.Entities.Dtos;
using Prueba.Domain.Interfaces;

namespace Prueba.Adapters.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _categoryService.GetAll();
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<List<CategoryDto>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
