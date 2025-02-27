using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaDocumentoEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AWM_DOCUMENTO_EMPRESA",
                columns: table => new
                {
                    CD = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_EMPRESA_ID = table.Column<int>(type: "INT", nullable: false),
                    CD_DOCUMENTO_ID = table.Column<int>(type: "INT", nullable: false),
                    STATUS = table.Column<int>(type: "INT", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_DOCUMENTO_EMPRESA", x => x.CD);
                    table.ForeignKey(
                        name: "FK_AWM_DOCUMENTO_EMPRESA_AWM_EMPRESA_CD_EMPRESA_ID",
                        column: x => x.CD_EMPRESA_ID,
                        principalTable: "AWM_EMPRESA",
                        principalColumn: "CD_EMPRESA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AWM_DOCUMENTO_EMPRESA_CD_EMPRESA_ID",
                table: "AWM_DOCUMENTO_EMPRESA",
                column: "CD_EMPRESA_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWM_DOCUMENTO_EMPRESA");
        }
    }
}
