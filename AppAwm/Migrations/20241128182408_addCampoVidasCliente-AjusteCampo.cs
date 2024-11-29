using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addCampoVidasClienteAjusteCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_USUARIO",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_OBRA",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_EMPRESA",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_USUARIO",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_OBRA",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_EMPRESA",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "INT",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);
        }
    }
}
