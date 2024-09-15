using Microsoft.EntityFrameworkCore;
using Reservas.Domain.Entities;

namespace Reservas.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("usuarios", "usuarioservicio"); 

            modelBuilder.Entity<Rol>()
                .ToTable("roles", "usuarioservicio"); 

          
            modelBuilder.Entity<Notificacion>()
                .ToTable("notificaciones", "notificacionservicio");

            modelBuilder.Entity<Usuario>()
        .HasKey(d => d.usuarioid);

        }
    }
}
