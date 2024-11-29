using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cd_DocumentoComplementar_Id",
                schema: "dbo",
                table: "AWM_DOCUMENTO_COMPLEMENTAR",
                newName: "CD_DOCUMENTOCOMPLEMENTAR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CD_DOCUMENTOCOMPLEMENTAR_ID",
                schema: "dbo",
                table: "AWM_DOCUMENTO_COMPLEMENTAR",
                newName: "Cd_DocumentoComplementar_Id");
        }
    }
}
