using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class addTableLogErros_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AWM_LOG_EXCEPTION",
                columns: table => new
                {
                    CD_CARGO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    METODO = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    ORIGEMTRACE = table.Column<string>(type: "VARCHAR(300)", nullable: true),
                    DATAEXCEPTION = table.Column<DateTime>(type: "DATE", nullable: false),
                    ERROR = table.Column<string>(type: "VARCHAR(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_LOG_EXCEPTION", x => x.CD_CARGO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWM_LOG_EXCEPTION");
        }
    }
}
