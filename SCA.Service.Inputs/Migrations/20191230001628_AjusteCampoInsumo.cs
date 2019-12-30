using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.Service.Inputs.Migrations
{
    public partial class AjusteCampoInsumo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Marca_MarcaId",
                table: "Insumo");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Tipo_TipoId",
                table: "Insumo");

            migrationBuilder.DropColumn(
                name: "DataAquisicao",
                table: "Insumo");

            migrationBuilder.AlterColumn<int>(
                name: "TipoId",
                table: "Insumo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "Insumo",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Insumo",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Insumo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Insumo_Marca_MarcaId",
                table: "Insumo",
                column: "MarcaId",
                principalTable: "Marca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insumo_Tipo_TipoId",
                table: "Insumo",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Marca_MarcaId",
                table: "Insumo");

            migrationBuilder.DropForeignKey(
                name: "FK_Insumo_Tipo_TipoId",
                table: "Insumo");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Insumo");

            migrationBuilder.AlterColumn<int>(
                name: "TipoId",
                table: "Insumo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MarcaId",
                table: "Insumo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Insumo",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAquisicao",
                table: "Insumo",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Insumo_Marca_MarcaId",
                table: "Insumo",
                column: "MarcaId",
                principalTable: "Marca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Insumo_Tipo_TipoId",
                table: "Insumo",
                column: "TipoId",
                principalTable: "Tipo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
