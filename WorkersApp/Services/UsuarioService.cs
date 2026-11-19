using Microsoft.AspNetCore.Identity;
using WorkersApp.Models;
using WorkersApp.Repository;

namespace WorkersApp.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly PasswordHasher<UsuarioRequestDto> _passwordHasher;
        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = new PasswordHasher<UsuarioRequestDto>();
        }

        public async Task<string?> Login(string username, string password)
        {
            var usuarioRequest = await _usuarioRepository.GetClaveByUsername(username);

            if (usuarioRequest == null)
            {
                return null;
            }

            var verificar = _passwordHasher.VerifyHashedPassword(usuarioRequest, usuarioRequest.Clave, password);

            if(verificar == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return usuarioRequest.Username;

        }
    }
}
