using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class AjusteV02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AlterColumn<int>(
                name: "CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.CreateIndex(
                name: "IX_AWM_FUNCIONARIO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                column: "CARGO",
                unique: true,
                filter: "[CARGO] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<int>(
                name: "CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AWM_FUNCIONARIO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                column: "CARGO",
                unique: true);
        }
    }
}
