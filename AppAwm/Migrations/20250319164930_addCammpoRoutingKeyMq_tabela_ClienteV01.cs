using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addCammpoRoutingKeyMq_tabela_ClienteV01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ROUTINGKEYMQ",
                table: "AWM_CLIENTE",
                type: "VARCHAR(30)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 22);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ROUTINGKEYMQ",
                table: "AWM_CLIENTE");
        }
    }
}
