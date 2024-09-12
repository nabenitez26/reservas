using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reservas.Domain.Repositories;
using Reservas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Reservas.Domain.Services.Interfaces;

namespace UsuarioServicio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserService _usuarioService;

        public UsuariosController(IUserService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        
        [HttpGet("admin-data")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuarioAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _usuarioService.AddUsuarioAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.usuarioid }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.usuarioid)
            {
                return BadRequest();
            }

            await _usuarioService.UpdateUsuarioAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _usuarioService.DeleteUsuarioAsync(id);
            return NoContent();
        }
    }
}
