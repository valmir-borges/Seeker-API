using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet("GetAllUsuarios")]
        public async Task<ActionResult<List<UsuarioModel>>> GetAllUsuarios()
        {
            List<UsuarioModel> users = await _usuarioRepositorio.GetAll();
            return Ok(users);
        }

        [HttpGet("GetUsuarioId/{id}")]
        public async Task<ActionResult<UsuarioModel>> GetUsuarioId(int id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.GetById(id);
            return Ok(usuario);
        }

        [HttpPost("CreateUsuario")]
        public async Task<ActionResult<UsuarioModel>> InsertUsuario([FromBody] UsuarioModel usuarioModel)
        {
            UsuarioModel user = await _usuarioRepositorio.InsertUsuario(usuarioModel);
            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioModel>> GetByEmail([FromBody] UsuarioModel usuarioModel)
        {
            // Verificando se o usuário mandou o email e a senha
            if (usuarioModel == null || string.IsNullOrEmpty(usuarioModel.UsuarioEmail) || string.IsNullOrEmpty(usuarioModel.UsuarioSenha))
            {
                return BadRequest("Email e senha são obrigatórios.");
            }

            // Executando o método que criei no repositório para pegar o usuário pelo Email
            UsuarioModel userLogin = await _usuarioRepositorio.GetByEmail(usuarioModel.UsuarioEmail);

            // Colocando o usuário encontrado a partir do email dentro de uma variável userLogin
            if (userLogin == null)
            {
                return Unauthorized("Usuário não encontrado.");
            }

            // Verificando a senha do usuário, a partir do que está no banco (userLogin) e com o que foi mandado (usuarioModel)
            if (userLogin.UsuarioSenha == usuarioModel.UsuarioSenha)
            {
                // Retornando o usuário encontrado
                return Ok(userLogin);
            }
            else
            {
                return Unauthorized("Senha incorreta.");
            }
        }



        [HttpPut("UpdateUsuario/{id:int}")]
        public async Task<ActionResult<UsuarioModel>> UpdateUsuario(int id, [FromBody] UsuarioModel usuarioModel)
        {
            usuarioModel.UsuarioId = id;
            UsuarioModel usuario = await _usuarioRepositorio.UpdateUsuario(usuarioModel, id);
            return Ok(usuario);
        }

        [HttpDelete("DeleteUsuario/{id:int}")]

        public async Task<ActionResult<UsuarioModel>> DeleteUsuario(int id)
        {
            bool deleted = await _usuarioRepositorio.DeleteUsuario(id);
            return Ok(deleted);
        }
    }
}
