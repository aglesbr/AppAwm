using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class newTable_Download : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AWM_DOWNLOAD",
                columns: table => new
                {
                    CD_DOWNLOAD = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    ANEXO = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "DATE", nullable: false),
                    CD_USUARIO_CRIACAO = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_DOWNLOAD", x => x.CD_DOWNLOAD);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWM_DOWNLOAD");
        }
    }
}
