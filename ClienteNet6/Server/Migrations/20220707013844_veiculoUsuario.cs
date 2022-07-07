using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteNet6.Server.Migrations
{
    public partial class veiculoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioVeiculo",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdVeiculo = table.Column<int>(type: "int(17)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioVeiculo", x => new { x.IdVeiculo, x.IdUser });
                    table.ForeignKey(
                        name: "FK_UsuarioVeiculo_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioVeiculo_Veiculo_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculo",
                        principalColumn: "Renavam",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "latin1");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioVeiculo_IdUser",
                table: "UsuarioVeiculo",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioVeiculo");
        }
    }
}
