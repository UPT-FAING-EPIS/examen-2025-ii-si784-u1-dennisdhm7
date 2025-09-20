using Matricula.Api.Data;
using Matricula.Api.Dtos;
using Matricula.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Matricula.Api.Controllers;

[ApiController]
[Route("courses")] // rutas en inglés
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _contexto;

    public CoursesController(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    // GET /courses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> ObtenerCursos()
    {
        return Ok(await _contexto.Courses.ToListAsync());
    }

    // GET /courses/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Course>> ObtenerCurso(int id)
    {
        var curso = await _contexto.Courses.FindAsync(id);
        return curso is null ? NotFound() : Ok(curso);
    }

    // POST /courses
    [Authorize(Roles = "admin,instructor")]
    [HttpPost]
    public async Task<ActionResult<Course>> CrearCurso([FromBody] CrearCursoDto dto)
    {
        var curso = new Course
        {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Categoria = dto.Categoria,
            Nivel = dto.Nivel,
            Instructor = dto.Instructor,
            Precio = dto.Precio,
            DuracionHoras = dto.DuracionHoras
        };

        _contexto.Courses.Add(curso);
        await _contexto.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerCurso), new { id = curso.Id }, curso);
    }

    // PUT /courses/{id}
    [Authorize(Roles = "admin,instructor")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> EditarCurso(int id, [FromBody] CrearCursoDto dto)
    {
        var curso = await _contexto.Courses.FindAsync(id);
        if (curso is null) return NotFound();

        curso.Titulo = dto.Titulo;
        curso.Descripcion = dto.Descripcion;
        curso.Categoria = dto.Categoria;
        curso.Nivel = dto.Nivel;
        curso.Instructor = dto.Instructor;
        curso.Precio = dto.Precio;
        curso.DuracionHoras = dto.DuracionHoras;

        await _contexto.SaveChangesAsync();
        return NoContent();
    }

    // DELETE /courses/{id}
    [Authorize(Roles = "admin,instructor")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarCurso(int id)
    {
        var curso = await _contexto.Courses.FindAsync(id);
        if (curso is null) return NotFound();

        _contexto.Courses.Remove(curso);
        await _contexto.SaveChangesAsync();

        return NoContent();
    }
}
