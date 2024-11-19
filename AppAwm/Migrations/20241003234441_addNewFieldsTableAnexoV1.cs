using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addNewFieldsTableAnexoV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CD_USUARIO_ANALISTA",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MOTIVO_REJEICAO",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "VARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TIPO_ANEXO",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "VARCHAR(4)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CD_USUARIO_ANALISTA",
                schema: "dbo",
                table: "AWM_ANEXO");

            migrationBuilder.DropColumn(
                name: "MOTIVO_REJEICAO",
                schema: "dbo",
                table: "AWM_ANEXO");

            migrationBuilder.DropColumn(
                name: "STATUS",
                schema: "dbo",
                table: "AWM_ANEXO");

            migrationBuilder.DropColumn(
                name: "TIPO_ANEXO",
                schema: "dbo",
                table: "AWM_ANEXO");
        }
    }
}
