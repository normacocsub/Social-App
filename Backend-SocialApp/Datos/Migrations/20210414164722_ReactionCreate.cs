using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class ReactionCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reaccion",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Like = table.Column<bool>(type: "bit", nullable: false),
                    Love = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reaccion_PublicacionIdPublicacion",
                table: "Reaccion",
                column: "PublicacionIdPublicacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reaccion");
        }
    }
}
