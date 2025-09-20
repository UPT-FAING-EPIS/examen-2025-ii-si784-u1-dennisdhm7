using Matricula.Api.Data;
using Matricula.Api.Dtos;
using Matricula.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Matricula.Api.Controllers;

[ApiController]
[Route("auth")] // rutas en inglés
public class AuthController : ControllerBase
{
    private readonly AppDbContext _contexto;
    private readonly IConfiguration _config;

    public AuthController(AppDbContext contexto, IConfiguration config)
    {
        _contexto = contexto;
        _config = config;
    }

    // POST /auth/login
    [HttpPost("login")]
    public IActionResult IniciarSesion([FromBody] LoginDto dto)
    {
        var usuario = _contexto.Users
            .FirstOrDefault(u => u.Correo == dto.Correo && u.Clave == dto.Clave);

        if (usuario is null) return Unauthorized(new { mensaje = "Credenciales inválidas" });

        var reclamos = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Correo),
            new Claim("uid", usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, usuario.Rol)
        };

        var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: reclamos,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credenciales
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            rol = usuario.Rol,
            usuarioId = usuario.Id
        });
    }
}
