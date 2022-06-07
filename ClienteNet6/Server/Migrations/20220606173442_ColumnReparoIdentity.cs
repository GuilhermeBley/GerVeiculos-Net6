using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClienteNet6.Server.Migrations
{
    public partial class ColumnReparoIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "NomeEmpresa",
                keyValue: null,
                column: "NomeEmpresa",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "NomeEmpresa",
                table: "AspNetUsers",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "latin1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NomeEmpresa",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)")
                .Annotation("MySql:CharSet", "latin1")
                .OldAnnotation("MySql:CharSet", "latin1");
        }
    }
}
