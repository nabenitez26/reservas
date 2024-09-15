using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using NotificacionServicio.Controllers;
using Notificacion.Domain.Services.Interfaces;
using Notificacionentity = Notificacion.Domain.Entities.Notificacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UTest.Controllers
{
    public class NotificationControllerTests
    {
        private readonly NotificationController _controller;
        private readonly Mock<INotificacionService> _notificacionServiceMock;

        public NotificationControllerTests()
        {
            _notificacionServiceMock = new Mock<INotificacionService>();
            _controller = new NotificationController(_notificacionServiceMock.Object);
        }

        [Fact]
        public async Task GetAllNotificaciones_ReturnsOkResult_WithListOfNotificaciones()
        {
            // Arrange: configuramos el mock para que devuelva una lista de notificaciones simulada
            var notificaciones = new List<Notificacionentity>
            {
                new Notificacionentity { notificacionid = 1, mensaje = "Mensaje 1", usuarioid = 1, tipo = "test" },
                new Notificacionentity { notificacionid = 2, mensaje = "Mensaje 2", usuarioid = 2, tipo = "test" }
            };
            _notificacionServiceMock.Setup(s => s.GetAllNotificacionesAsync()).ReturnsAsync(notificaciones);

            // Act: llamamos al método que queremos probar
            var result = await _controller.GetAllNotificaciones();

            // Assert: verificamos que el resultado sea OkObjectResult y contiene la lista esperada
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnNotificaciones = Assert.IsType<List<Notificacionentity>>(okResult.Value);
            Assert.Equal(2, returnNotificaciones.Count);  // Comprobamos que la lista tenga dos notificaciones
        }

        [Fact]
        public async Task GetNotificacion_ExistingId_ReturnsOkResult_WithNotificacion()
        {
            var notificacion = new Notificacionentity { notificacionid = 1, mensaje = "Mensaje 1", usuarioid = 1, tipo = "test" };
            _notificacionServiceMock.Setup(s => s.GetNotificacionByIdAsync(1)).ReturnsAsync(notificacion);

            var result = await _controller.GetNotificacionById(1);

            // Assert: verificamos que el resultado sea OkObjectResult y contiene la notificación esperada
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnNotificacion = Assert.IsType<Notificacionentity>(okResult.Value);
            Assert.Equal(1, returnNotificacion.notificacionid);
            Assert.Equal("Mensaje 1", returnNotificacion.mensaje);
        }

        [Fact]
        public async Task GetNotificacion_NonExistingId_ReturnsNotFoundResult()
        {
            _notificacionServiceMock.Setup(s => s.GetNotificacionByIdAsync(2)).ReturnsAsync((Notificacionentity)null);
            var result = await _controller.GetNotificacionById(2);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddNotificacion_ValidNotificacion_ReturnsCreatedAtAction()
        {
            // Arrange: configuramos el mock para que simule la creación de una nueva notificación
            var notificacion = new Notificacionentity { notificacionid = 1, mensaje = "Mensaje 1", usuarioid = 1 };

            // Act: llamamos al método que queremos probar
            var result = await _controller.AddNotificacion(notificacion);

            // Assert: verificamos que el resultado sea CreatedAtActionResult
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnNotificacion = Assert.IsType<Notificacionentity>(createdAtActionResult.Value);
            Assert.Equal(1, returnNotificacion.notificacionid);
        }

        [Fact]
        public async Task UpdateNotificacion_ValidNotificacion_ReturnsNoContent()
        {
            var notificacion = new Notificacionentity { notificacionid = 1, mensaje = "Mensaje actualizado", usuarioid = 1, tipo = "test" };

            _notificacionServiceMock.Setup(s => s.UpdateNotificacionAsync(notificacion)).Returns(Task.CompletedTask);

            // Act: llamamos al método que queremos probar
            var result = await _controller.UpdateNotificacion(1, notificacion);

            // Assert: verificamos que el resultado sea NoContentResult
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateNotificacion_IdMismatch_ReturnsBadRequest()
        {
            var notificacion = new Notificacionentity { notificacionid = 1, mensaje = "Mensaje actualizado", usuarioid = 1, tipo = "test" };
            var result = await _controller.UpdateNotificacion(2, notificacion);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeleteNotificacion_ValidId_ReturnsNoContent()
        {
            _notificacionServiceMock.Setup(s => s.DeleteNotificacionAsync(1)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteNotificacion(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}