using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCA.Service.Monitoring.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regiao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UF = table.Column<string>(maxLength: 2, nullable: true),
                    Cidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Barragem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RegiaoId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barragem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barragem_Regiao_RegiaoId",
                        column: x => x.RegiaoId,
                        principalTable: "Regiao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BarragemId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensor_Barragem_BarragemId",
                        column: x => x.BarragemId,
                        principalTable: "Barragem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SensorHistorico",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SensorId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorHistorico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorHistorico_Sensor_SensorId",
                        column: x => x.SensorId,
                        principalTable: "Sensor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Barragem_RegiaoId",
                table: "Barragem",
                column: "RegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensor_BarragemId",
                table: "Sensor",
                column: "BarragemId");

            migrationBuilder.CreateIndex(
                name: "IX_SensorHistorico_SensorId",
                table: "SensorHistorico",
                column: "SensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorHistorico");

            migrationBuilder.DropTable(
                name: "Sensor");

            migrationBuilder.DropTable(
                name: "Barragem");

            migrationBuilder.DropTable(
                name: "Regiao");
        }
    }
}
