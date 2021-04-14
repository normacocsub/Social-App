using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class NewCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePerfil",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Comentarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Comentarios");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImagePerfil",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
