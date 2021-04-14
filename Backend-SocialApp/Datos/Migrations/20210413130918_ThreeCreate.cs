using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class ThreeCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Publicacions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Publicacions");
        }
    }
}
