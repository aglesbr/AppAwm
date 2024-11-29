using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addCampoStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "STATUS",
                schema: "dbo",
                table: "AWM_DOCUMENTO_CARGO",
                type: "BIT",
                nullable: false,
                defaultValue: true)
                .Annotation("Relational:ColumnOrder", 15);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STATUS",
                schema: "dbo",
                table: "AWM_DOCUMENTO_CARGO");
        }
    }
}
