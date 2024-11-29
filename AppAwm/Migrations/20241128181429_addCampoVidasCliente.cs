using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addCampoVidasCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PLANO_PACOTE_VIDAS",
                schema: "dbo",
                table: "AWM_CLIENTE",
                type: "VARCHAR(50)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 26);

            migrationBuilder.AddColumn<int>(
                name: "PLANO_VIDAS",
                schema: "dbo",
                table: "AWM_CLIENTE",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 27);

            migrationBuilder.AddColumn<int>(
                name: "PLANO_VIDAS_ATIVADAS",
                schema: "dbo",
                table: "AWM_CLIENTE",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 28);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PLANO_PACOTE_VIDAS",
                schema: "dbo",
                table: "AWM_CLIENTE");

            migrationBuilder.DropColumn(
                name: "PLANO_VIDAS",
                schema: "dbo",
                table: "AWM_CLIENTE");

            migrationBuilder.DropColumn(
                name: "PLANO_VIDAS_ATIVADAS",
                schema: "dbo",
                table: "AWM_CLIENTE");
        }
    }
}
