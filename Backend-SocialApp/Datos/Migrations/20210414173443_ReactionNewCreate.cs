using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class ReactionNewCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdUsuario",
                table: "Reaccion",
                type: "varchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reaccion_IdUsuario",
                table: "Reaccion",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Reaccion_Usuarios_IdUsuario",
                table: "Reaccion",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Correo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reaccion_Usuarios_IdUsuario",
                table: "Reaccion");

            migrationBuilder.DropIndex(
                name: "IX_Reaccion_IdUsuario",
                table: "Reaccion");

            migrationBuilder.AlterColumn<string>(
                name: "IdUsuario",
                table: "Reaccion",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldNullable: true);
        }
    }
}
