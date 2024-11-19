using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class AjustenosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AWM_FUNCIONARIO_AWM_CARGO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            migrationBuilder.DropIndex(
                name: "IX_AWM_FUNCIONARIO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            migrationBuilder.AlterColumn<int>(
                name: "TIPOCONTRATO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AWM_FUNCIONARIO_AWM_CARGO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                column: "CARGO",
                principalSchema: "dbo",
                principalTable: "AWM_CARGO",
                principalColumn: "CD_CARGO",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "TIPOCONTRATO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AWM_FUNCIONARIO_AWM_CARGO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                column: "CARGO",
                principalSchema: "dbo",
                principalTable: "AWM_CARGO",
                principalColumn: "CD_CARGO");
        }
    }
}
