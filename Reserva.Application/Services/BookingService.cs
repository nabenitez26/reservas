using Reserva.Domain.Repositories;
using ReservaEntity = Reserva.Domain.Entities.Reserva;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reserva.Domain.Services.Interfaces;

namespace Reserva.Application.Services
{
    public class BookingService: IBookingService
    {
        private readonly IBookingRepository _reservaRepository;

        public BookingService(IBookingRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public async Task<IEnumerable<ReservaEntity>> GetAllReservasAsync()
        {
            return await _reservaRepository.GetAllAsync();
        }

        public async Task<ReservaEntity> GetReservaByIdAsync(int id)
        {
            return await _reservaRepository.GetByIdAsync(id);
        }

        public async Task AddReservaAsync(ReservaEntity reserva)
        {
            await _reservaRepository.AddAsync(reserva);
        }

        public async Task UpdateReservaAsync(ReservaEntity reserva)
        {
            await _reservaRepository.UpdateAsync(reserva);
        }

        public async Task DeleteReservaAsync(int id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva != null)
            {
                await _reservaRepository.DeleteAsync(reserva);
            }
        }
    }
}