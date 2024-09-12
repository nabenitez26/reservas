using ReservaEntity = Reserva.Domain.Entities.Reserva;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reserva.Domain.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<ReservaEntity>> GetAllReservasAsync();
        Task<ReservaEntity> GetReservaByIdAsync(int id);
        Task AddReservaAsync(ReservaEntity reserva);
        Task UpdateReservaAsync(ReservaEntity reserva);
        Task DeleteReservaAsync(int id);
    }
}
