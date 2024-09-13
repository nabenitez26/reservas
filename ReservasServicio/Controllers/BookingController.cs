using Microsoft.AspNetCore.Mvc;
using ReservaEntity = Reserva.Domain.Entities.Reserva;
using Reserva.Domain.Entities;
using Reserva.Domain.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ReservasServicio.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _reservaService;

        public BookingController(IBookingService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet("admin-data")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<IEnumerable<ReservaEntity>>> GetAllReservas()
        {
            var reservas = await _reservaService.GetAllReservasAsync();
            return Ok(reservas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaEntity>> GetReserva(int id)
        {
            var reserva = await _reservaService.GetReservaByIdAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        public async Task<ActionResult> AddReserva(ReservaEntity reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _reservaService.AddReservaAsync(reserva);
            return CreatedAtAction(nameof(GetReserva), new { id = reserva.reservaid }, reserva);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> UpdateReserva(int id, ReservaEntity reserva)
        {
            if (id != reserva.reservaid)
            {
                return BadRequest("El ID de la reserva no coincide.");
            }

            await _reservaService.UpdateReservaAsync(reserva);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            await _reservaService.DeleteReservaAsync(id);
            return NoContent();
        }
    }
}