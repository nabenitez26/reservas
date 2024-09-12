using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservas.Domain.Entities;
using Reservas.Domain.Repositories;
using Reservas.Domain.Services.Interfaces;

namespace Reservas.Application.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _usuarioRepository;
        private readonly PasswordService _passwordService;
        public UserServices(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _passwordService = new PasswordService();
        }
        public async Task<IEnumerable<Usuario>> GetAllUsuarioAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            usuario.contrasenahash = _passwordService.HashPassword(usuario.contrasenahash);
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task UpdateUsuarioAsync(Usuario usuario)
        {
            usuario.contrasenahash = _passwordService.HashPassword(usuario.contrasenahash);
            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public Usuario ValidateUserCredentials(string username, string password)
        {
            var user = _usuarioRepository.GetUserByUsername(username);
            if (user == null)
            {
                return null;
            }
            bool isPasswordValid = _passwordService.VerifyPassword(password, user.contrasenahash);
            if (!isPasswordValid)
            {
                return null; 
            }
            return user; 
        }
    }
}