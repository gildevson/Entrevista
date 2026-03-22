using Microsoft.AspNetCore.Mvc;
using Entrevista.Model;

namespace Entrevista.Controllers {
    [ApiController]
    [Route("api/Usuarios")]
    public class UsuarioController : ControllerBase {
        private static List<Usuario> usuarios = new List<Usuario>();

        /*
         private:
         significa que a variável só pode ser acessada dentro desta própria classe.

         static:
         significa que a lista é compartilhada pela classe inteira.
         Ou seja, ela não depende de criar um objeto da classe para existir.
         Enquanto a aplicação estiver rodando, essa lista ficará em memória.

         UsuarioController : ControllerBase
         significa que UsuarioController herda de ControllerBase.

         ControllerBase:
         vem do namespace Microsoft.AspNetCore.Mvc.
         Ela fornece recursos prontos para criar APIs, como Ok(), BadRequest(), NotFound() etc.

         IActionResult:
         é um tipo de retorno usado para devolver respostas HTTP.
         Exemplo: Ok(), BadRequest(), NotFound().

         [ApiController]:
         indica que esta classe é um controller de API.

         [Route]:
         define a rota base do controller.

         [HttpGet]:
         indica que o método responde a requisições do tipo GET.

         [HttpPost]:
         indica que o método responde a requisições do tipo POST.

         Ok():
         retorna resposta HTTP 200 (sucesso), geralmente com dados no corpo da resposta.
        */

        [HttpPost("CriarUsuario")]
        public IActionResult CriarUsuario(Usuario usuario) {
            usuario.id = Guid.NewGuid();
            usuarios.Add(usuario);
            return Ok(usuario);
        }

        [HttpGet("todosUsuarios")]
        public IActionResult GetUsuarios() {
            return Ok(usuarios);
        }

        [HttpPut("AtualizarUsuario/{id}")]
        public IActionResult AtualizarUsuario(Guid id, Usuario usuarioAtualizado) {
            var usuario = usuarios.FirstOrDefault(u => u.id == id);

            if (usuario == null) {
                return NotFound("Usuario não encontrado");
            }

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.CPF = usuarioAtualizado.CPF;
            usuario.Data = usuarioAtualizado.Data;

            return Ok(usuario);
        }

        [HttpDelete("DeleteUsuario/{id}")]
        public IActionResult Delete(Guid id) {
            var usuario = usuarios.FirstOrDefault(u => u.id == id);

            if(usuario == null) {
                return NotFound("Usuario não existe");
            }

            if(id == null) {
                return NotFound("Usuario nem existe");
            }

            usuarios.Remove(usuario);
            return Ok("Usuario removido");

        }

    }
}