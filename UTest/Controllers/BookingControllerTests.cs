using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ReservasServicio.Controllers;
using Reserva.Domain.Services.Interfaces;
using Reserva.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reserva.Application.Services;
using Reservaentity = Reserva.Domain.Entities.Reserva;

namespace UTest.Controllers
{
    public class BookingControllerTests
    {
        private readonly BookingController _controller;
        private readonly Mock<IBookingService> _reservaServiceMock;

        public BookingControllerTests()
        {
            _reservaServiceMock = new Mock<IBookingService>();
            _controller = new BookingController(_reservaServiceMock.Object);
        }

        [Fact]
        public async Task GetAllReservas_ReturnsOkResult_WithListOfReservas()
        {
            var reservas = new List<Reservaentity>
            {
                new Reservaentity { reservaid = 1, usuarioid = 1, estado = "Confirmada" },
                new Reservaentity { reservaid = 2, usuarioid = 2, estado = "Pendiente" }
            };
            _reservaServiceMock.Setup(s => s.GetAllReservasAsync()).ReturnsAsync(reservas);

            var result = await _controller.GetAllReservas();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnReservas = Assert.IsType<List<Reservaentity>>(okResult.Value);
            Assert.Equal(2, returnReservas.Count); 
        }

        [Fact]
        public async Task GetReserva_ExistingId_ReturnsOkResult_WithReserva()
        {
            var reserva = new Reservaentity { reservaid = 1, usuarioid = 1, estado = "Confirmada" };
            _reservaServiceMock.Setup(s => s.GetReservaByIdAsync(1)).ReturnsAsync(reserva);

            var result = await _controller.GetReservabyByIdAsync(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnReserva = Assert.IsType<Reservaentity>(okResult.Value);
            Assert.Equal(1, returnReserva.reservaid);
            Assert.Equal("Confirmada", returnReserva.estado);
        }

        [Fact]
        public async Task GetReserva_NonExistingId_ReturnsNotFoundResult()
        {
            _reservaServiceMock.Setup(s => s.GetReservaByIdAsync(2)).ReturnsAsync((Reservaentity)null);
            var result = await _controller.GetReservabyByIdAsync(2);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddReserva_ValidReserva_ReturnsCreatedAtAction()
        {
            var reserva = new Reservaentity { reservaid = 1, usuarioid = 1, estado = "Confirmada" };
            var result = await _controller.AddReserva(reserva);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnReserva = Assert.IsType<Reservaentity>(createdAtActionResult.Value);
            Assert.Equal(1, returnReserva.reservaid);
        }

        [Fact]
        public async Task UpdateReserva_ValidReserva_ReturnsNoContent()
        {
            var reserva = new Reservaentity { reservaid = 1, usuarioid = 1, estado = "Confirmada" };
            _reservaServiceMock.Setup(s => s.UpdateReservaAsync(reserva)).Returns(Task.CompletedTask);
            var result = await _controller.UpdateReserva(1, reserva);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateReserva_IdMismatch_ReturnsBadRequest()
        {
            var reserva = new Reservaentity { reservaid = 1, usuarioid = 1, estado = "Confirmada" };
            var result = await _controller.UpdateReserva(2, reserva);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteReserva_ValidId_ReturnsNoContent()
        {
            _reservaServiceMock.Setup(s => s.DeleteReservaAsync(1)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteReserva(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}