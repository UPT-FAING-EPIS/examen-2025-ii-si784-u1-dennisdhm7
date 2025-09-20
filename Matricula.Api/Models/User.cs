namespace Matricula.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Correo { get; set; } = "";
        public string Clave { get; set; } = "";
        public string NombreCompleto { get; set; } = "";
        public string Rol { get; set; } = "usuario"; // admin | instructor | usuario
    }
}
