using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Publicacions_PublicacionId",
                table: "Comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentario_Usuarios_IdUsuario",
                table: "Comentario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario");

            migrationBuilder.RenameTable(
                name: "Comentario",
                newName: "Comentarios");

            migrationBuilder.RenameIndex(
                name: "IX_Comentario_PublicacionId",
                table: "Comentarios",
                newName: "IX_Comentarios_PublicacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentario_IdUsuario",
                table: "Comentarios",
                newName: "IX_Comentarios_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios",
                column: "IdComentario");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Publicacions_PublicacionId",
                table: "Comentarios",
                column: "PublicacionId",
                principalTable: "Publicacions",
                principalColumn: "IdPublicacion",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Usuarios_IdUsuario",
                table: "Comentarios",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Correo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Publicacions_PublicacionId",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Usuarios_IdUsuario",
                table: "Comentarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comentarios",
                table: "Comentarios");

            migrationBuilder.RenameTable(
                name: "Comentarios",
                newName: "Comentario");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_PublicacionId",
                table: "Comentario",
                newName: "IX_Comentario_PublicacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_IdUsuario",
                table: "Comentario",
                newName: "IX_Comentario_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comentario",
                table: "Comentario",
                column: "IdComentario");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Publicacions_PublicacionId",
                table: "Comentario",
                column: "PublicacionId",
                principalTable: "Publicacions",
                principalColumn: "IdPublicacion",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentario_Usuarios_IdUsuario",
                table: "Comentario",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Correo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
