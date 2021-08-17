using Microsoft.EntityFrameworkCore.Migrations;

namespace FavoDeMel.Venda.Data.Migrations
{
    public partial class AdicionapropriedadeItemStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemStatus",
                table: "ComandaItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemStatus",
                table: "ComandaItems");
        }
    }
}
