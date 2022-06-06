using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteNet6.Server.Migrations
{
    public partial class veiculosInfracoesInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "latin1");

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Renavam = table.Column<int>(type: "int(17)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Chassi = table.Column<string>(type: "char(17)", nullable: false)
                        .Annotation("MySql:CharSet", "latin1"),
                    Placa = table.Column<string>(type: "char(7)", nullable: false)
                        .Annotation("MySql:CharSet", "latin1"),
                    Cor = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1"),
                    Modelo = table.Column<string>(type: "varchar(45)", nullable: true)
                        .Annotation("MySql:CharSet", "latin1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Renavam);
                })
                .Annotation("MySql:CharSet", "latin1");

            migrationBuilder.CreateTable(
                name: "Infracao",
                columns: table => new
                {
                    Ait = table.Column<string>(type: "varchar(25)", nullable: false)
                        .Annotation("MySql:CharSet", "latin1"),
                    Renavam = table.Column<int>(type: "int(17)", nullable: false),
                    Local = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "latin1"),
                    Emissao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Validade = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infracao", x => x.Ait);
                    table.ForeignKey(
                        name: "FK_Infracao_Veiculo_Renavam",
                        column: x => x.Renavam,
                        principalTable: "Veiculo",
                        principalColumn: "Renavam",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1");

            migrationBuilder.CreateIndex(
                name: "IX_Infracao_Renavam",
                table: "Infracao",
                column: "Renavam");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_Chassi",
                table: "Veiculo",
                column: "Chassi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_Placa",
                table: "Veiculo",
                column: "Placa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Infracao");

            migrationBuilder.DropTable(
                name: "Veiculo");
        }
    }
}
