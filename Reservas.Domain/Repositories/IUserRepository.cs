
using Reservas.Domain.Entities;

namespace Reservas.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Usuario GetUserByUsername(string username);
    }
}
