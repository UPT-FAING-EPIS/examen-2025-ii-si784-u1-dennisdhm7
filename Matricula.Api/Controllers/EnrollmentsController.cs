using Matricula.Api.Data;
using Matricula.Api.Dtos;
using Matricula.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Matricula.Api.Controllers;

[ApiController]
[Route("enrollments")] // rutas en inglés
public class EnrollmentsController : ControllerBase
{
    private readonly AppDbContext _contexto;

    public EnrollmentsController(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    // POST /enrollments
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Enrollment>> CrearInscripcion([FromBody] InscripcionDto dto)
    {
        var usuarioId = int.Parse(User.FindFirstValue("uid") ?? "0");

        var existeCurso = await _contexto.Courses.AnyAsync(c => c.Id == dto.CursoId);
        if (!existeCurso) return NotFound(new { mensaje = "Curso no encontrado" });

        var yaInscrito = await _contexto.Enrollments
            .AnyAsync(i => i.UsuarioId == usuarioId && i.CursoId == dto.CursoId);

        if (yaInscrito) return Conflict(new { mensaje = "Ya inscrito en este curso" });

        var inscripcion = new Enrollment { UsuarioId = usuarioId, CursoId = dto.CursoId };
        _contexto.Enrollments.Add(inscripcion);
        await _contexto.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerInscripcionesUsuario), new { userId = usuarioId }, inscripcion);
    }

    // GET /enrollments/{userId}
    [Authorize]
    [HttpGet("{userId:int}")]
    public async Task<ActionResult<IEnumerable<Enrollment>>> ObtenerInscripcionesUsuario(int userId)
    {
        var uid = int.Parse(User.FindFirstValue("uid") ?? "0");
        var esAdmin = User.IsInRole("admin");

        if (uid != userId && !esAdmin) return Forbid();

        var inscripciones = await _contexto.Enrollments
            .Where(i => i.UsuarioId == userId)
            .ToListAsync();

        return Ok(inscripciones);
    }
}
