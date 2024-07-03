using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaSalud.data.migrations
{
    /// <inheritdoc />
    public partial class TratamientosYPadecimientosPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PacientePadecimiento",
                columns: table => new
                {
                    PacienteID = table.Column<int>(type: "int", nullable: false),
                    PadecimientoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacientePadecimiento", x => new { x.PacienteID, x.PadecimientoID });
                    table.ForeignKey(
                        name: "FK_PacientePadecimiento_Paciente_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Paciente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacientePadecimiento_Padecimiento_PadecimientoID",
                        column: x => x.PadecimientoID,
                        principalTable: "Padecimiento",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PacienteTratamiento",
                columns: table => new
                {
                    PacienteID = table.Column<int>(type: "int", nullable: false),
                    TratamientoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteTratamiento", x => new { x.PacienteID, x.TratamientoID });
                    table.ForeignKey(
                        name: "FK_PacienteTratamiento_Paciente_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Paciente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacienteTratamiento_Tratamiento_TratamientoID",
                        column: x => x.TratamientoID,
                        principalTable: "Tratamiento",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacientePadecimiento_PadecimientoID",
                table: "PacientePadecimiento",
                column: "PadecimientoID");

            migrationBuilder.CreateIndex(
                name: "IX_PacienteTratamiento_TratamientoID",
                table: "PacienteTratamiento",
                column: "TratamientoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacientePadecimiento");

            migrationBuilder.DropTable(
                name: "PacienteTratamiento");
        }
    }
}
