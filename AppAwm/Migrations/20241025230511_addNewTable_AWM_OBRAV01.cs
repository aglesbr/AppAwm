using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addNewTable_AWM_OBRAV01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AWM_OBRA",
                schema: "dbo",
                columns: table => new
                {
                    CD_OBRA = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_EMPRESA_ID = table.Column<int>(type: "INT", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    STATUS = table.Column<bool>(type: "bit", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CD_USUARIO_CRIACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    CD_USUARIO_ATUALIZACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_OBRA", x => x.CD_OBRA);
                    table.ForeignKey(
                        name: "FK_AWM_OBRA_AWM_EMPRESA_CD_EMPRESA_ID",
                        column: x => x.CD_EMPRESA_ID,
                        principalSchema: "dbo",
                        principalTable: "AWM_EMPRESA",
                        principalColumn: "CD_EMPRESA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AWM_OBRA_CD_EMPRESA_ID",
                schema: "dbo",
                table: "AWM_OBRA",
                column: "CD_EMPRESA_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWM_OBRA",
                schema: "dbo");
        }
    }
}
