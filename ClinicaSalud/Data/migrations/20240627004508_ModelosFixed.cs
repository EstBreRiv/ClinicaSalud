using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaSalud.data.migrations
{
    /// <inheritdoc />
    public partial class ModelosFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Tratamiento",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Tratamiento",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Medicamento",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Tratamiento");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Tratamiento");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Medicamento");
        }
    }
}
