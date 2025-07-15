using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prueba.Domain.Entities.Dtos;
using Prueba.Domain.Entities.Model;
using Prueba.Domain.Entities.Request;
using Prueba.Domain.Interfaces;

namespace Prueba.Adapters.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        public ProductsController(IProductsService productsService, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var result = _productsService.GetById(id) ?? throw new Exception("No existe producto con ese id");
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<ProductsDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var result = _productsService.GetAllData();
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<List<ProductsDto>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] ProductRequest product)
        {
            try
            {
                var result = _productsService.Create(_mapper.Map<Products>(product));
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<ProductsDto>(result));
            }
            catch (NullReferenceException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] ProductRequest payload)
        {
            try
            {
                var product = _mapper.Map<Products>(payload);
                var result = _productsService.UpdateProduct(id, product);
                return StatusCode(StatusCodes.Status200OK, _mapper.Map<ProductsDto>(result));
            }
            catch (NullReferenceException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = _productsService.Delete(id);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
