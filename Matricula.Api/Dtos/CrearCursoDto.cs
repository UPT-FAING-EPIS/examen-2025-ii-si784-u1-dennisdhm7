namespace Matricula.Api.Dtos;

public record CrearCursoDto(
    string Titulo,
    string? Descripcion,
    string Categoria,
    string Nivel,
    string Instructor,
    decimal Precio,
    int DuracionHoras
);
