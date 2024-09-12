using Microsoft.EntityFrameworkCore;
using ReservaEntity = Reserva.Domain.Entities.Reserva;
using Reserva.Domain.Repositories;
using Reserva.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reserva.Infrastructure.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReservaEntity> GetByIdAsync(int id)
        {
            return await _context.reservas
                .Include(r => r.DetallesReserva) // Cargar los detalles de la reserva
                .FirstOrDefaultAsync(r => r.reservaid == id);
        }

        public async Task<IEnumerable<ReservaEntity>> GetAllAsync()
        {
            return await _context.reservas
                .Include(r => r.DetallesReserva) // Cargar los detalles de todas las reservas
                .ToListAsync();
        }

        public async Task AddAsync(ReservaEntity reserva)
        {
            await _context.reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReservaEntity reserva)
        {
            _context.reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ReservaEntity reserva)
        {
            _context.reservas.Remove(reserva);
            await _context.SaveChangesAsync();
        }
    }
}