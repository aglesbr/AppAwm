using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class AjusteV03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_AWM_FUNCIONARIO_AWM_CARGO_CARGO",
            //    schema: "dbo",
            //    table: "AWM_FUNCIONARIO");

            migrationBuilder.DropIndex(
                name: "IX_AWM_FUNCIONARIO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            //migrationBuilder.AddColumn<int>(
            //    name: "FuncionarioCd_Funcionario",
            //    schema: "dbo",
            //    table: "AWM_CARGO",
            //    type: "INT",
            //    nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AWM_CARGO_FuncionarioCd_Funcionario",
                schema: "dbo",
                table: "AWM_CARGO",
                column: "FuncionarioCd_Funcionario");

            migrationBuilder.AddForeignKey(
                name: "FK_AWM_CARGO_AWM_FUNCIONARIO_FuncionarioCd_Funcionario",
                schema: "dbo",
                table: "AWM_CARGO",
                column: "FuncionarioCd_Funcionario",
                principalSchema: "dbo",
                principalTable: "AWM_FUNCIONARIO",
                principalColumn: "CD_FUNCIONARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AWM_CARGO_AWM_FUNCIONARIO_FuncionarioCd_Funcionario",
                schema: "dbo",
                table: "AWM_CARGO");

            migrationBuilder.DropIndex(
                name: "IX_AWM_CARGO_FuncionarioCd_Funcionario",
                schema: "dbo",
                table: "AWM_CARGO");

            migrationBuilder.DropColumn(
                name: "FuncionarioCd_Funcionario",
                schema: "dbo",
                table: "AWM_CARGO");

            migrationBuilder.CreateIndex(
                name: "IX_AWM_FUNCIONARIO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                column: "CARGO",
                unique: true,
                filter: "[CARGO] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AWM_FUNCIONARIO_AWM_CARGO_CARGO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                column: "CARGO",
                principalSchema: "dbo",
                principalTable: "AWM_CARGO",
                principalColumn: "CD_CARGO");
        }
    }
}
