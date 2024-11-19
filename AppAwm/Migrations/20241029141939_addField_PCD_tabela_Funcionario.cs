using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addField_PCD_tabela_Funcionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PCD",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "bit",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 46);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PCD",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");
        }
    }
}
