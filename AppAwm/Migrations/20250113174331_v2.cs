using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CD_USUARIO_ATUALIZACAO",
                table: "AWM_CLIENTE",
                type: "VARCHAR(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CD_USUARIO_CRIACAO",
                table: "AWM_CLIENTE",
                type: "VARCHAR(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "AWM_CLIENTE",
                type: "VARCHAR(14)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 6);

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_ATUALIZACAO",
                table: "AWM_CLIENTE",
                type: "DATE",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_CRIACAO",
                table: "AWM_CLIENTE",
                type: "DATE",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<ulong>(
                name: "PERIODO_TESTE",
                table: "AWM_CLIENTE",
                type: "BIT",
                nullable: false,
                defaultValue: 0ul)
                .Annotation("Relational:ColumnOrder", 60);

            migrationBuilder.AddColumn<DateTime>(
                name: "VENCIMENTO_PERIODO_TESTE",
                table: "AWM_CLIENTE",
                type: "DATE",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 65);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CD_USUARIO_ATUALIZACAO",
                table: "AWM_CLIENTE");

            migrationBuilder.DropColumn(
                name: "CD_USUARIO_CRIACAO",
                table: "AWM_CLIENTE");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "AWM_CLIENTE");

            migrationBuilder.DropColumn(
                name: "DT_ATUALIZACAO",
                table: "AWM_CLIENTE");

            migrationBuilder.DropColumn(
                name: "DT_CRIACAO",
                table: "AWM_CLIENTE");

            migrationBuilder.DropColumn(
                name: "PERIODO_TESTE",
                table: "AWM_CLIENTE");

            migrationBuilder.DropColumn(
                name: "VENCIMENTO_PERIODO_TESTE",
                table: "AWM_CLIENTE");
        }
    }
}
