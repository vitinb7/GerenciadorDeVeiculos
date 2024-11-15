using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCarro.Migrations
{
    public partial class RemoverImagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "carros");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "carros",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
