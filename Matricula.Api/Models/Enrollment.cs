namespace Matricula.Api.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int CursoId { get; set; }
        public DateTime FechaInscripcion { get; set; } = DateTime.UtcNow;
    }
}
