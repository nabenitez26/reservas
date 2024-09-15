using NotificationEntity = Notificacion.Domain.Entities.Notificacion;
using Notificacion.Domain.Repositories;
using Notificacion.Domain.Services.Interfaces;

namespace Notificacion.Application.Services
{
    public class NotificationService : INotificacionService
    {
        private readonly INotificacionRepository _notificacionRepository;

        public NotificationService(INotificacionRepository notificacionRepository)
        {
            _notificacionRepository = notificacionRepository;
        }

        public async Task<IEnumerable<NotificationEntity>> GetAllNotificacionesAsync()
        {
            return await _notificacionRepository.GetAllAsync();
        }

        public async Task<NotificationEntity> GetNotificacionByIdAsync(int id)
        {
            return await _notificacionRepository.GetByIdAsync(id);
        }

        public async Task AddNotificacionAsync(NotificationEntity notificacion)
        {
            await _notificacionRepository.AddAsync(notificacion);
        }

        public async Task UpdateNotificacionAsync(NotificationEntity notificacion)
        {
            await _notificacionRepository.UpdateAsync(notificacion);
        }

        public async Task DeleteNotificacionAsync(int id)
        {
            await _notificacionRepository.DeleteAsync(id);
        }
    }
}
