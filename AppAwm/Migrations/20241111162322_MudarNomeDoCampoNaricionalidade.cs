using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class MudarNomeDoCampoNaricionalidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NACIONALIDADE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                newName: "ESTRANGEIRO");

            migrationBuilder.AlterColumn<bool>(
                name: "ESTRANGEIRO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "BIT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ESTRANGEIRO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                newName: "NACIONALIDADE");

            migrationBuilder.AlterColumn<int>(
                name: "NACIONALIDADE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BIT");
        }
    }
}
