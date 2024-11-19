using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class RemoverCampoAptidao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TREINAMENTOS_APTIDOES",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TREINAMENTOS_APTIDOES",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "VARCHAR(700)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 41);
        }
    }
}
