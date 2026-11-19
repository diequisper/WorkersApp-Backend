using Microsoft.AspNetCore.Mvc;
using WorkersApp.Models;
using WorkersApp.Services;

namespace WorkersApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService; 
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UsuarioRequestDto usuarioRequest)
        {
            string? serviceResponse = await _usuarioService.Login(usuarioRequest.Username, usuarioRequest.Clave);

            if (serviceResponse == null)
            {
                return StatusCode(400, new { message = "Usuario o contraseña incorrectos"});
            }

            return StatusCode(200, new { result =  serviceResponse, message = "Bienvenido"});
        }
    }
}
