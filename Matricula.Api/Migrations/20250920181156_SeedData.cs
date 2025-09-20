using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matricula.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Categoria", "Descripcion", "DuracionHoras", "Instructor", "Nivel", "Precio", "Titulo" },
                values: new object[] { 1, "Programación", "Aprende ASP.NET Core y Angular paso a paso", 40, "Ing. Lanchipa", "Intermedio", 150.00m, "Curso de Programación Web" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Clave", "Correo", "NombreCompleto", "Rol" },
                values: new object[] { 1, "123456", "admin@upt.edu.pe", "Administrador General", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
