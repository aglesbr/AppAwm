using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class AddCampoNacionalidadeV04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PISPASEP",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            migrationBuilder.DropColumn(
                name: "REMUNERACAO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            migrationBuilder.AddColumn<int>(
                name: "NACIONALIDADE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 16);

            migrationBuilder.CreateIndex(
                name: "IX_AWM_FUNCIONARIO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                column: "CARGO",
                unique: true,
                filter: "[CARGO] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AWM_FUNCIONARIO_AWM_CARGO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                column: "CARGO",
                principalSchema: "dbo",
                principalTable: "AWM_CARGO",
                principalColumn: "CD_CARGO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AWM_FUNCIONARIO_AWM_CARGO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            migrationBuilder.DropIndex(
                name: "IX_AWM_FUNCIONARIO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            migrationBuilder.DropColumn(
                name: "NACIONALIDADE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            migrationBuilder.AddColumn<string>(
                name: "PISPASEP",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "VARCHAR(15)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 10);

            migrationBuilder.AddColumn<decimal>(
                name: "REMUNERACAO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "DECIMAL(8,2)",
                nullable: false,
                defaultValue: 0m)
                .Annotation("Relational:ColumnOrder", 45);
        }
    }
}
