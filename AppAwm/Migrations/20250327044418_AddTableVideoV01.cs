using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class AddTableVideoV01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AWM_VIDEO",
                columns: table => new
                {
                    CD_VIDEO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITULO = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    URL = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    STATUS = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_VIDEO", x => x.CD_VIDEO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWM_VIDEO");
        }
    }
}
