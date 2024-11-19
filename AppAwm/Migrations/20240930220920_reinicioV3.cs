using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class reinicioV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
