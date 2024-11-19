using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addTreinamentoA_justeV15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWM_TREINAMENTO_HABILIDADE",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_TREINAMENTO",
                schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AWM_TREINAMENTO",
                schema: "dbo",
                columns: table => new
                {
                    CD_TREINAMENTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_TREINAMENTO", x => x.CD_TREINAMENTO);
                });

            migrationBuilder.CreateTable(
                name: "AWM_TREINAMENTO_HABILIDADE",
                schema: "dbo",
                columns: table => new
                {
                    CD_HABILIDADE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_TREINAMENTO_ID = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_TREINAMENTO_HABILIDADE", x => x.CD_HABILIDADE);
                    table.ForeignKey(
                        name: "FK_AWM_TREINAMENTO_HABILIDADE_AWM_TREINAMENTO_CD_TREINAMENTO_ID",
                        column: x => x.CD_TREINAMENTO_ID,
                        principalSchema: "dbo",
                        principalTable: "AWM_TREINAMENTO",
                        principalColumn: "CD_TREINAMENTO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AWM_TREINAMENTO_HABILIDADE_CD_TREINAMENTO_ID",
                schema: "dbo",
                table: "AWM_TREINAMENTO_HABILIDADE",
                column: "CD_TREINAMENTO_ID");
        }
    }
}
