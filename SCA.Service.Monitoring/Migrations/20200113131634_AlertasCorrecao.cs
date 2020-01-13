using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.Service.Monitoring.Migrations
{
    public partial class AlertasCorrecao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cadastro_Regiao_RegiaoId",
                table: "Cadastro");

            migrationBuilder.DropColumn(
                name: "RegiaId",
                table: "Cadastro");

            migrationBuilder.AlterColumn<int>(
                name: "RegiaoId",
                table: "Cadastro",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cadastro_Regiao_RegiaoId",
                table: "Cadastro",
                column: "RegiaoId",
                principalTable: "Regiao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cadastro_Regiao_RegiaoId",
                table: "Cadastro");

            migrationBuilder.AlterColumn<int>(
                name: "RegiaoId",
                table: "Cadastro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RegiaId",
                table: "Cadastro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cadastro_Regiao_RegiaoId",
                table: "Cadastro",
                column: "RegiaoId",
                principalTable: "Regiao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
