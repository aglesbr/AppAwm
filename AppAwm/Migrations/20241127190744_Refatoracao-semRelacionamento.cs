using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class RefatoracaosemRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "dbo",
                table: "AWM_ANEXO",
                newName: "STATUS");

            migrationBuilder.AddColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_USUARIO",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_OBRA",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 95);

            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_OBRA",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 100);

            migrationBuilder.AddColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 95);

            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 100);

            migrationBuilder.AddColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 95);

            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 100);

            migrationBuilder.AddColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_EMPRESA",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 95);

            migrationBuilder.AddColumn<int>(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_EMPRESA",
                type: "INT",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 100);

            migrationBuilder.AlterColumn<int>(
                name: "STATUS",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AWM_CLIENTE",
                schema: "dbo",
                columns: table => new
                {
                    CD_CLIENTE = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    USUARIOMQ = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    PASSWORDMQ = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    HOSTMQ = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_CLIENTE", x => x.CD_CLIENTE);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWM_CLIENTE",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_USUARIO");

            migrationBuilder.DropColumn(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_OBRA");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_OBRA");

            migrationBuilder.DropColumn(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA");

            migrationBuilder.DropColumn(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_FUNCIONARIO");

            migrationBuilder.DropColumn(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_EMPRESA");

            migrationBuilder.DropColumn(
                name: "ID_USUARIO_CRIACAO",
                schema: "dbo",
                table: "AWM_EMPRESA");

            migrationBuilder.DropColumn(
                name: "CD_CLIENTE",
                schema: "dbo",
                table: "AWM_ANEXO");

            migrationBuilder.RenameColumn(
                name: "STATUS",
                schema: "dbo",
                table: "AWM_ANEXO",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "dbo",
                table: "AWM_ANEXO",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);
        }
    }
}
