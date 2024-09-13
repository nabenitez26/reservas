using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificacionEntity = Notificacion.Domain.Entities.Notificacion;
using Notificacion.Domain.Services.Interfaces;
using System.Collections.Generic;

namespace NotificacionServicio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificacionService _notificacionService;

        public NotificationController(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacionEntity>>> GetAllNotificaciones()
        {
            var notificaciones = await _notificacionService.GetAllNotificacionesAsync();
            return Ok(notificaciones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificacionEntity>> GetNotificacionById(int id)
        {
            var notificacion = await _notificacionService.GetNotificacionByIdAsync(id);
            if (notificacion == null)
            {
                return NotFound();
            }
            return Ok(notificacion);
        }

        [HttpPost]
        [Authorize(Roles = "1, 2")] 
        public async Task<ActionResult> AddNotificacion(NotificacionEntity notificacion)
        {
            await _notificacionService.AddNotificacionAsync(notificacion);
            return CreatedAtAction(nameof(GetNotificacionById), new { id = notificacion.notificacionid }, notificacion);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1, 2")]
        public async Task<IActionResult> UpdateNotificacion(int id, NotificacionEntity notificacion)
        {
            if (id != notificacion.notificacionid)
            {
                return BadRequest("El ID de la notificación no coincide.");
            }

            await _notificacionService.UpdateNotificacionAsync(notificacion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            await _notificacionService.DeleteNotificacionAsync(id);
            return NoContent();
        }
    }
}