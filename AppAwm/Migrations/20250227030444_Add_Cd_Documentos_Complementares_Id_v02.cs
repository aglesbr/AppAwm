using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class Add_Cd_Documentos_Complementares_Id_v02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CD_DOCUMENTOS_COMPLEMENTARES_ID",
                table: "AWM_DOCUMENTO_EMPRESA",
                type: "VARCHAR(80)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 11);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CD_DOCUMENTOS_COMPLEMENTARES_ID",
                table: "AWM_DOCUMENTO_EMPRESA");
        }
    }
}
