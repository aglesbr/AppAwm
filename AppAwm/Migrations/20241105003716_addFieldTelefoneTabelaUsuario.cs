using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addFieldTelefoneTabelaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TELEFONE",
                schema: "dbo",
                table: "AWM_USUARIO",
                type: "VARCHAR(16)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "PCD",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TELEFONE",
                schema: "dbo",
                table: "AWM_USUARIO");

            migrationBuilder.AlterColumn<bool>(
                name: "PCD",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
