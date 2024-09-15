using ReservaEntity = Reserva.Domain.Entities.Reserva;
namespace Reserva.Domain.Repositories
{
    public interface IBookingRepository
    {
        Task<ReservaEntity> GetByIdAsync(int id);
        Task<IEnumerable<ReservaEntity>> GetAllAsync();
        Task AddAsync(ReservaEntity reserva);
        Task UpdateAsync(ReservaEntity reserva);
        Task DeleteAsync(ReservaEntity reserva);
    }
}
