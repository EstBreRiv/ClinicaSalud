using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaSalud.data.migrations
{
    /// <inheritdoc />
    public partial class pacienteMedicamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PacienteMedicamento",
                columns: table => new
                {
                    PacienteID = table.Column<int>(type: "int", nullable: false),
                    MedicamentoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteMedicamento", x => new { x.PacienteID, x.MedicamentoID });
                    table.ForeignKey(
                        name: "FK_PacienteMedicamento_Medicamento_MedicamentoID",
                        column: x => x.MedicamentoID,
                        principalTable: "Medicamento",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacienteMedicamento_Paciente_PacienteID",
                        column: x => x.PacienteID,
                        principalTable: "Paciente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacienteMedicamento_MedicamentoID",
                table: "PacienteMedicamento",
                column: "MedicamentoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacienteMedicamento");
        }
    }
}
