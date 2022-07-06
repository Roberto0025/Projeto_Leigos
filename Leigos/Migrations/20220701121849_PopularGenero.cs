using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leigos.Migrations
{
    public partial class PopularGenero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Generos",
                column: "GeneroNome",
                value: "FEMININO"
            );
            migrationBuilder.InsertData(
                table: "Generos",
                column: "GeneroNome",
                value: "MASCULINO"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Generos");
        }
    }
}
