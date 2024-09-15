using Reservas.Domain.Entities;
using Reservas.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservas.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task AddUsuarioAsync(Usuario usuario);
        Task UpdateUsuarioAsync(Usuario usuario);
        Task DeleteUsuarioAsync(int id);
        Usuario ValidateUserCredentials(string username, string password);
        Task<IEnumerable<Usuario>> GetAllUsuarioAsync();
    }
}
