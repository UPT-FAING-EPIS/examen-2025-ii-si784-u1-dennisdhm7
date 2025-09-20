using System.ComponentModel.DataAnnotations.Schema;

namespace Matricula.Api.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string? Descripcion { get; set; }
        public string Categoria { get; set; } = "";
        public string Nivel { get; set; } = "";
        public string Instructor { get; set; } = "";
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }
        public int DuracionHoras { get; set; }
    }
}
