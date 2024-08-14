using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cambiocampoIdPersona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gestiones_Personas_PersonaId",
                table: "Gestiones");

            migrationBuilder.DropIndex(
                name: "IX_Gestiones_PersonaId",
                table: "Gestiones");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "Gestiones",
                newName: "IdPersona");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Personas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Gestiones_Id",
                table: "Personas",
                column: "Id",
                principalTable: "Gestiones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Gestiones_Id",
                table: "Personas");

            migrationBuilder.RenameColumn(
                name: "IdPersona",
                table: "Gestiones",
                newName: "PersonaId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Personas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

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
    }
}
