using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class RefatoracaoComRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AWM_USUARIO_CD_CLIENTE",
                schema: "dbo",
                table: "AWM_USUARIO",
                column: "CD_CLIENTE");

            migrationBuilder.AddForeignKey(
                name: "FK_AWM_USUARIO_AWM_CLIENTE_CD_CLIENTE",
                schema: "dbo",
                table: "AWM_USUARIO",
                column: "CD_CLIENTE",
                principalSchema: "dbo",
                principalTable: "AWM_CLIENTE",
                principalColumn: "CD_CLIENTE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AWM_USUARIO_AWM_CLIENTE_CD_CLIENTE",
                schema: "dbo",
                table: "AWM_USUARIO");

            migrationBuilder.DropIndex(
                name: "IX_AWM_USUARIO_CD_CLIENTE",
                schema: "dbo",
                table: "AWM_USUARIO");
        }
    }
}
