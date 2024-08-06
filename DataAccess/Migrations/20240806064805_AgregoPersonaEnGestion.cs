using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AgregoPersonaEnGestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonaId",
                table: "Gestiones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Gestiones_PersonaId",
                table: "Gestiones",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gestiones_Personas_PersonaId",
                table: "Gestiones",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gestiones_Personas_PersonaId",
                table: "Gestiones");

            migrationBuilder.DropIndex(
                name: "IX_Gestiones_PersonaId",
                table: "Gestiones");

            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "Gestiones");
        }
    }
}
