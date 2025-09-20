using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matricula.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedAlumnoInscripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CursoId", "FechaInscripcion", "UsuarioId" },
                values: new object[] { 1, 1, new DateTime(2025, 9, 20, 13, 13, 39, 108, DateTimeKind.Local).AddTicks(9411), 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Clave", "Correo", "NombreCompleto", "Rol" },
                values: new object[] { 2, "123456", "alumno@upt.edu.pe", "Alumno de Prueba", "alumno" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
