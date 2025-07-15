using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prueba.Domain.Entities.Dtos;
using Prueba.Domain.Interfaces;

namespace Prueba.Adapters.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;

        public StatusController(IStatusService statusService, IMapper mapper, ILogger<StatusController> logger)
        {
            _statusService = statusService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _statusService.GetAll();
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
