using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reaccion_Publicacions_PublicacionIdPublicacion",
                table: "Reaccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Reaccion_Usuarios_IdUsuario",
                table: "Reaccion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reaccion",
                table: "Reaccion");

            migrationBuilder.RenameTable(
                name: "Reaccion",
                newName: "Reacciones");

            migrationBuilder.RenameIndex(
                name: "IX_Reaccion_PublicacionIdPublicacion",
                table: "Reacciones",
                newName: "IX_Reacciones_PublicacionIdPublicacion");

            migrationBuilder.RenameIndex(
                name: "IX_Reaccion_IdUsuario",
                table: "Reacciones",
                newName: "IX_Reacciones_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reacciones",
                table: "Reacciones",
                column: "Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Reacciones_Publicacions_PublicacionIdPublicacion",
                table: "Reacciones",
                column: "PublicacionIdPublicacion",
                principalTable: "Publicacions",
                principalColumn: "IdPublicacion",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reacciones_Usuarios_IdUsuario",
                table: "Reacciones",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Correo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reacciones_Publicacions_PublicacionIdPublicacion",
                table: "Reacciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Reacciones_Usuarios_IdUsuario",
                table: "Reacciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reacciones",
                table: "Reacciones");

            migrationBuilder.RenameTable(
                name: "Reacciones",
                newName: "Reaccion");

            migrationBuilder.RenameIndex(
                name: "IX_Reacciones_PublicacionIdPublicacion",
                table: "Reaccion",
                newName: "IX_Reaccion_PublicacionIdPublicacion");

            migrationBuilder.RenameIndex(
                name: "IX_Reacciones_IdUsuario",
                table: "Reaccion",
                newName: "IX_Reaccion_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reaccion",
                table: "Reaccion",
                column: "Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Reaccion_Publicacions_PublicacionIdPublicacion",
                table: "Reaccion",
                column: "PublicacionIdPublicacion",
                principalTable: "Publicacions",
                principalColumn: "IdPublicacion",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reaccion_Usuarios_IdUsuario",
                table: "Reaccion",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Correo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
