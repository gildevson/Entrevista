using Microsoft.AspNetCore.Mvc;
using Entrevista.Model;

namespace Entrevista.Controllers {

    [ApiController]
    [Route("api/controller")]
    public class UsuarioController : ControllerBase {
        private static List<Usuario> usuarios = new List<Usuario>();

        [HttpGet("todosusuarios")]
        public IActionResult GetUsuarios() { //k
            return Ok(usuarios);

        }
        [HttpPost("CriarUsuario")]
        public IActionResult CriarUsuario(Usuario usuario) {
                usuario.id = Guid.NewGuid();
                usuarios.Add(usuario);

                return Ok(usuario);
        }
    }

}
