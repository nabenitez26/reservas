using Microsoft.EntityFrameworkCore;
using Reserva.Domain.Entities;
using ReservaEntity = Reserva.Domain.Entities.Reserva;

namespace Reserva.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReservaEntity> reservas { get; set; }
        public DbSet<DetalleReserva> detallesreserva { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla para la entidad ReservaEntity
            modelBuilder.Entity<ReservaEntity>()
                .ToTable("reservas", "reservaservicio");

            // Configuración de la tabla para la entidad DetalleReserva
            modelBuilder.Entity<DetalleReserva>()
                .ToTable("detallesreserva", "reservaservicio");

            // Configuración de propiedades adicionales
            modelBuilder.Entity<ReservaEntity>()
                .Property(r => r.estado)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<DetalleReserva>()
                .Property(d => d.servicio)
                .HasMaxLength(100)
                .IsRequired();

            // Configuración de la clave primaria para DetalleReserva
            modelBuilder.Entity<DetalleReserva>()
                .HasKey(dr => dr.detallereservaid);
        }
    }
}