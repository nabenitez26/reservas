using NotificationEntity = Notificacion.Domain.Entities.Notificacion;

namespace Notificacion.Domain.Repositories
{
    public interface INotificacionRepository
    {
        Task<IEnumerable<NotificationEntity>> GetAllAsync();
        Task<NotificationEntity> GetByIdAsync(int id);
        Task AddAsync(NotificationEntity notificacion);
        Task UpdateAsync(NotificationEntity notificacion);
        Task DeleteAsync(int id);
    }
}
