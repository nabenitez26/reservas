using Microsoft.EntityFrameworkCore;
using Notificacion.Domain.Entities;
using NotificacionEntity = Notificacion.Domain.Entities.Notificacion;

namespace Notificacion.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NotificacionEntity> Notificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificacionEntity>().ToTable("notificaciones", "notificacionservicio");

            modelBuilder.Entity<NotificacionEntity>()
                .Property(n => n.tipo)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<NotificacionEntity>()
                .Property(n => n.mensaje)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}