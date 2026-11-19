using Microsoft.AspNetCore.Mvc;
using WorkersApp.Models;
using WorkersApp.Services;

namespace WorkersApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadorController : Controller
    {
        private readonly TrabajadorService _trabajadorService;

        public TrabajadorController(TrabajadorService trabajadorService)
        {
            _trabajadorService = trabajadorService;
        }

        [HttpPost("getByDni")]
        public async Task<IActionResult> GetByDni([FromBody] string? dni)
        {
            List<Trabajador> serviceResponse = await _trabajadorService.GetById(dni);

            return StatusCode(200, serviceResponse);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Trabajador trabajador)
        {
            GenericServiceResponse<object> serviceResponse = await _trabajadorService.CreateTrabajador(trabajador);

            if(serviceResponse.Status != GenericServiceResponse<object>.Result.Success)
            {
                switch (serviceResponse.Status)
                {
                    case GenericServiceResponse<object>.Result.InvalidData:
                        return StatusCode(400, serviceResponse.Response);
                    case GenericServiceResponse<object>.Result.OperationFailed:
                        return StatusCode(500, serviceResponse.Response);
                    default:
                        return StatusCode(500, serviceResponse.Response);
                }
            }

            return StatusCode(201, serviceResponse.Response);
        }
    }
}
