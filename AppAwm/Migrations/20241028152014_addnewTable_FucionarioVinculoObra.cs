using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addnewTable_FucionarioVinculoObra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AWM_FUNCIONARIO_VINCULO_OBRA",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_FUNCIONARIO_ID = table.Column<int>(type: "int", nullable: false),
                    CD_EMPRESA_ID = table.Column<int>(type: "int", nullable: false),
                    CD_OBRA_ID = table.Column<int>(type: "int", nullable: false),
                    VINCULADO = table.Column<bool>(type: "bit", nullable: false),
                    DT_VINCULADO = table.Column<DateTime>(type: "DATE", nullable: false),
                    DT_DESVINCULADO = table.Column<DateTime>(type: "DATE", nullable: false),
                    CD_USUARIO_CRIACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    CD_USUARIO_ATUALIZACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_FUNCIONARIO_VINCULO_OBRA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AWM_FUNCIONARIO_VINCULO_OBRA_AWM_FUNCIONARIO_CD_FUNCIONARIO_ID",
                        column: x => x.CD_FUNCIONARIO_ID,
                        principalSchema: "dbo",
                        principalTable: "AWM_FUNCIONARIO",
                        principalColumn: "CD_FUNCIONARIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AWM_FUNCIONARIO_VINCULO_OBRA_CD_FUNCIONARIO_ID",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                column: "CD_FUNCIONARIO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWM_FUNCIONARIO_VINCULO_OBRA",
                schema: "dbo");
        }
    }
}
