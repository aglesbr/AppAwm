using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addnewTable_FucionarioVinculoObraV01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_VINCULADO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                type: "DATE",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_DESVINCULADO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                type: "DATE",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_VINCULADO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DT_DESVINCULADO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "DATE",
                oldNullable: true);
        }
    }
}
