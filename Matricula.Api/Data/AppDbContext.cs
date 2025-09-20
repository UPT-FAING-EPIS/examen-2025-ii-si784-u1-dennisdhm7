using Matricula.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Matricula.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opciones) : base(opciones) { }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 🔹 Configurar precisión del decimal
        modelBuilder.Entity<Course>()
            .Property(c => c.Precio)
            .HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);

        // Usuario Admin inicial
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Correo = "admin@upt.edu.pe",
                Clave = "123456", // ⚠️ En un caso real debería estar hasheada
                NombreCompleto = "Administrador General",
                Rol = "admin"
            }
        );

        // Usuario Alumno inicial
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 2,
                Correo = "alumno@upt.edu.pe",
                Clave = "123456",
                NombreCompleto = "Alumno de Prueba",
                Rol = "alumno"
            }
        );

        // Curso de ejemplo inicial
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Titulo = "Curso de Programación Web",
                Descripcion = "Aprende ASP.NET Core y Angular paso a paso",
                Categoria = "Programación",
                Nivel = "Intermedio",
                Instructor = "Ing. Lanchipa",
                Precio = 150.00M,
                DuracionHoras = 40
            }
        );

        // Inscripción inicial: Alumno en el Curso de Programación Web
        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment
            {
                Id = 1,
                UsuarioId = 2,   // El alumno
                CursoId = 1,     // Curso inicial
                FechaInscripcion = DateTime.Now
            }
        );
    }
}
