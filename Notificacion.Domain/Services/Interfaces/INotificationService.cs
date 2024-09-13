using NotificactionEntity = Notificacion.Domain.Entities.Notificacion;

namespace Notificacion.Domain.Services.Interfaces
{
    public interface INotificacionService
    {
        Task<IEnumerable<NotificactionEntity>> GetAllNotificacionesAsync();
        Task<NotificactionEntity> GetNotificacionByIdAsync(int id);
        Task AddNotificacionAsync(NotificactionEntity notificacion);
        Task UpdateNotificacionAsync(NotificactionEntity notificacion);
        Task DeleteNotificacionAsync(int id);
    }
}
