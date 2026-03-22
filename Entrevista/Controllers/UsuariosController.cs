using Microsoft.AspNetCore.Mvc;
using Entrevista.Model;

namespace Entrevista.Controllers {

    // [ApiController]: marca a classe como um controller de API REST.
    // Ativa validações automáticas de modelo e outros comportamentos de API.
    [ApiController]

    // [Route]: define a rota base para todos os endpoints deste controller.
    // Todos os endpoints começarão com "api/Usuarios".
    [Route("api/Usuarios")]

    public class UsuarioController : ControllerBase {
        // Lista estática que simula um banco de dados em memória.
        // "private" → só acessível dentro desta classe.
        // "static" → compartilhada por todas as requisições enquanto a aplicação estiver rodando.
        // Atenção: ao reiniciar a aplicação, os dados são perdidos.
        private static List<Usuario> usuarios = new List<Usuario>();

        // ─────────────────────────────────────────────
        // CRIAR USUÁRIO
        // ─────────────────────────────────────────────
        // [HttpPost]: responde a requisições HTTP do tipo POST.
        // Usado para CRIAR novos recursos.
        // Rota completa: POST api/Usuarios/CriarUsuario
        [HttpPost("CriarUsuario")]
        public IActionResult CriarUsuario(Usuario usuario) {
            // Gera um ID único (GUID) para o novo usuário.
            // Isso evita que o cliente precise enviar o ID manualmente.
            usuario.id = Guid.NewGuid();

            // Adiciona o usuário na lista em memória.
            usuarios.Add(usuario);

            // Retorna HTTP 200 (OK) com o usuário criado no corpo da resposta.
            return Ok(usuario);
        }

        // ─────────────────────────────────────────────
        // LISTAR TODOS OS USUÁRIOS
        // ─────────────────────────────────────────────
        // [HttpGet]: responde a requisições HTTP do tipo GET.
        // Usado para CONSULTAR/LISTAR recursos existentes.
        // Rota completa: GET api/Usuarios/todosUsuarios
        [HttpGet("todosUsuarios")]
        public IActionResult GetUsuarios() {
            // Retorna HTTP 200 (OK) com a lista completa de usuários.
            return Ok(usuarios);
        }

        // ─────────────────────────────────────────────
        // ATUALIZAR USUÁRIO
        // ─────────────────────────────────────────────
        // [HttpPut]: responde a requisições HTTP do tipo PUT.
        // Usado para ATUALIZAR um recurso existente por completo.
        // {id} é um parâmetro dinâmico na URL. Ex: PUT api/Usuarios/AtualizarUsuario/abc-123
        [HttpPut("AtualizarUsuario/{id}")]
        public IActionResult AtualizarUsuario(Guid id, Usuario usuarioAtualizado) {
            // Busca o usuário na lista pelo ID recebido na URL.
            // FirstOrDefault retorna o primeiro que corresponder, ou null se não encontrar.
            var usuario = usuarios.FirstOrDefault(u => u.id == id);

            // Se não encontrou nenhum usuário com esse ID, retorna HTTP 404 (Not Found).
            if (usuario == null) {
                return NotFound("Usuario não encontrado");
            }

            // Atualiza cada campo do usuário encontrado com os novos dados recebidos no corpo da requisição.
            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.CPF = usuarioAtualizado.CPF;
            usuario.Data = usuarioAtualizado.Data;

            // Retorna HTTP 200 (OK) com o usuário já atualizado.
            return Ok(usuario);
        }

        // ─────────────────────────────────────────────
        // DELETAR USUÁRIO
        // ─────────────────────────────────────────────
        // [HttpDelete]: responde a requisições HTTP do tipo DELETE.
        // Usado para REMOVER um recurso existente.
        // {id} é o identificador do usuário a ser removido. Ex: DELETE api/Usuarios/DeleteUsuario/abc-123
        [HttpDelete("DeleteUsuario/{id}")]
        public IActionResult Delete(Guid id) {
            // Busca o usuário na lista pelo ID recebido.
            var usuario = usuarios.FirstOrDefault(u => u.id == id);

            // Se não encontrou o usuário, retorna HTTP 404 (Not Found).
            if (usuario == null) {
                return NotFound("Usuario não existe");
            }

            // ⚠️ ATENÇÃO — este segundo if é código morto (dead code):
            // Um Guid nunca pode ser null em C#, pois é um tipo por valor (struct).
            // Esta verificação jamais será verdadeira e nunca será executada.
            // Pode ser removida com segurança.
            if (id == null) {
                return NotFound("Usuario nem existe");
            }

            // Remove o usuário encontrado da lista em memória.
            usuarios.Remove(usuario);

            // Retorna HTTP 200 (OK) com uma mensagem de confirmação.
            return Ok("Usuario removido");
        }
    }
}