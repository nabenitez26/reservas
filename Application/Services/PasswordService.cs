using BCrypt.Net;

namespace Reservas.Application.Services
{
    public class PasswordService
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(password));
            }
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
            {
                throw new ArgumentException("La contraseña o el hash no pueden ser nulos o vacíos.");
            }
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}