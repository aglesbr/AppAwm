using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addIdColaborador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_ANEXO");
        }
    }
}
