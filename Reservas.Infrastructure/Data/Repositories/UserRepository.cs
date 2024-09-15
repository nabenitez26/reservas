using Reservas.Domain.Entities;
using Reservas.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Reservas.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }
        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
        public Usuario GetUserByUsername(string username)
        {
            return _context.Usuarios.SingleOrDefault(u => u.email == username);
        }
    }
}