using Microsoft.AspNetCore.Mvc;
using Moq;
using Reservas.Domain.Entities;
using Reservas.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioServicio.Controllers;

namespace UTest.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserService> _userServiceMock;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task GetAllUsuarios_ReturnsOkResult_WithListOfUsuarios()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { usuarioid = 1, nombre = "Juan", apellido = "Pepe", email = "test1@test.com", contrasenahash = "passwordtest", rolid = 1},
                new Usuario { usuarioid = 2, nombre = "Andres", apellido = "Perez",  email = "test2@test.com", contrasenahash = "passwordtest", rolid = 2}
            };
            _userServiceMock.Setup(s => s.GetAllUsuarioAsync()).ReturnsAsync(usuarios);
            var result = await _controller.GetAllUsuarios();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUsuarios = Assert.IsType<List<Usuario>>(okResult.Value);
            Assert.Equal(2, returnUsuarios.Count);  
        }

        [Fact]
        public async Task GetUsuario_ExistingId_ReturnsOkResult_WithUsuario()
        {
            var usuario = new Usuario { usuarioid = 1, nombre = "Juan", apellido = "Pepe", email = "test1@test.com", contrasenahash = "passwordtest", rolid = 1 };
            _userServiceMock.Setup(s => s.GetUsuarioByIdAsync(1)).ReturnsAsync(usuario);

            var result = await _controller.GetUsuario(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnUsuario = Assert.IsType<Usuario>(okResult.Value);
            Assert.Equal(1, returnUsuario.usuarioid);
            Assert.Equal("Juan", returnUsuario.nombre);
        }

        [Fact]
        public async Task GetUsuario_NonExistingId_ReturnsNotFoundResult()
        {
            _userServiceMock.Setup(s => s.GetUsuarioByIdAsync(2)).ReturnsAsync((Usuario)null);
            var result = await _controller.GetUsuario(2);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddUsuario_ValidUsuario_ReturnsCreatedAtAction()
        {
            var usuario = new Usuario { usuarioid = 1, nombre = "Juan", apellido = "Pepe" , email = "test1@test.com", contrasenahash = "passwordtest", rolid = 1 };
            var result = await _controller.AddUsuario(usuario);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnUsuario = Assert.IsType<Usuario>(createdAtActionResult.Value);
            Assert.Equal(1, returnUsuario.usuarioid);
        }

        [Fact]
        public async Task UpdateUsuario_ValidUsuario_ReturnsNoContent()
        {
            var usuario = new Usuario { usuarioid = 1, nombre = "Juan", apellido = "Perez", email = "test1@test.com", contrasenahash = "passwordtest", rolid = 1 };

            _userServiceMock.Setup(s => s.UpdateUsuarioAsync(usuario)).Returns(Task.CompletedTask);
            var result = await _controller.UpdateUsuario(1, usuario);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateUsuario_IdMismatch_ReturnsBadRequest()
        {
            var usuario = new Usuario { usuarioid = 1, nombre = "Juan", apellido = "Perez", email = "test1@test.com", contrasenahash = "passwordtest", rolid = 1 };
            var result = await _controller.UpdateUsuario(2, usuario);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteUsuario_ValidId_ReturnsNoContent()
        {
            _userServiceMock.Setup(s => s.DeleteUsuarioAsync(1)).Returns(Task.CompletedTask);
            var result = await _controller.DeleteUsuario(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}