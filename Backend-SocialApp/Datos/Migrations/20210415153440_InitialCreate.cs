using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Correo = table.Column<string>(type: "varchar(40)", nullable: false),
                    Password = table.Column<string>(type: "varchar(60)", nullable: true),
                    Nombres = table.Column<string>(type: "varchar(25)", nullable: true),
                    Apellidos = table.Column<string>(type: "varchar(25)", nullable: true),
                    Sexo = table.Column<string>(type: "varchar(9)", nullable: true),
                    KeyPasswordDesEncriptar = table.Column<string>(type: "varchar(16)", nullable: true),
                    ImagePerfil = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Correo);
                });

            migrationBuilder.CreateTable(
                name: "Publicacions",
                columns: table => new
                {
                    IdPublicacion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(25)", nullable: true),
                    ContenidoPublicacion = table.Column<string>(type: "varchar(500)", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUsuario = table.Column<string>(type: "varchar(40)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicacions", x => x.IdPublicacion);
                    table.ForeignKey(
                        name: "FK_Publicacions_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Correo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    IdComentario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContenidoComentario = table.Column<string>(type: "varchar(500)", nullable: true),
                    IdPublicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUsuario = table.Column<string>(type: "varchar(40)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublicacionIdPublicacion = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.IdComentario);
                    table.ForeignKey(
                        name: "FK_Comentarios_Publicacions_PublicacionIdPublicacion",
                        column: x => x.PublicacionIdPublicacion,
                        principalTable: "Publicacions",
                        principalColumn: "IdPublicacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Correo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reaccion",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Like = table.Column<bool>(type: "bit", nullable: false),
                    Love = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<string>(type: "varchar(40)", nullable: true),
                    PublicacionIdPublicacion = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reaccion", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Reaccion_Publicacions_PublicacionIdPublicacion",
                        column: x => x.PublicacionIdPublicacion,
                        principalTable: "Publicacions",
                        principalColumn: "IdPublicacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reaccion_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Correo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdUsuario",
                table: "Comentarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PublicacionIdPublicacion",
                table: "Comentarios",
                column: "PublicacionIdPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacions_IdUsuario",
                table: "Publicacions",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Reaccion_IdUsuario",
                table: "Reaccion",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Reaccion_PublicacionIdPublicacion",
                table: "Reaccion",
                column: "PublicacionIdPublicacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Reaccion");

            migrationBuilder.DropTable(
                name: "Publicacions");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
