using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class AjusteV07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");
        }
    }
}
