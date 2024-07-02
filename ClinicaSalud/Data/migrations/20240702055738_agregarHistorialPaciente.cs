using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaSalud.data.migrations
{
    /// <inheritdoc />
    public partial class agregarHistorialPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescripcionExamen",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HistorialClinico",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureURL",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionExamen",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "HistorialClinico",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "PictureURL",
                table: "Paciente");
        }
    }
}
