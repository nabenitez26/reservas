using Reservas.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Reservas.Domain.Services.Interfaces;
using Reservas.Domain.Entities;

namespace Reservas.Application.Services
{
    public class AuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public string Authenticate(string username, string password)
        {
            var user = _userService.ValidateUserCredentials(username, password);
            if (user == null)
            {
                return null; 
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.nombre),
                new Claim(ClaimTypes.Role, user.rolid.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "https://mitestreservas.com", 
                Audience = "servicio-usuarios,servicio-reservas,servicio-notificaciones" 
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}