using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.Service.Inputs.Migrations
{
    public partial class AddManutencaoPreventiva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManutencaoPreventiva",
                table: "Insumo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManutencaoPreventiva",
                table: "Insumo");
        }
    }
}
