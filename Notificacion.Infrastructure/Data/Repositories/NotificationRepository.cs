using Microsoft.EntityFrameworkCore;
using NotificacionEntity = Notificacion.Domain.Entities.Notificacion;
using Notificacion.Domain.Repositories;

namespace Notificacion.Infrastructure.Data.Repositories
{
    public class NotificationRepository : INotificacionRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificacionEntity>> GetAllAsync()
        {
            return await _context.Notificaciones.ToListAsync();
        }

        public async Task<NotificacionEntity> GetByIdAsync(int id)
        {
            return await _context.Notificaciones.FindAsync(id);
        }

        public async Task AddAsync(NotificacionEntity notificacion)
        {
            await _context.Notificaciones.AddAsync(notificacion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NotificacionEntity notificacion)
        {
            _context.Notificaciones.Update(notificacion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notificacion = await GetByIdAsync(id);
            if (notificacion != null)
            {
                _context.Notificaciones.Remove(notificacion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
