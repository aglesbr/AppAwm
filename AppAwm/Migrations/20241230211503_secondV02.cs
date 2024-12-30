using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAwm.Migrations
{
    /// <inheritdoc />
    public partial class secondV02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AWM_CARGO",
                schema: "dbo",
                columns: table => new
                {
                    CD_CARGO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    STATUS = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_CARGO", x => x.CD_CARGO);
                });

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
                    CANALMQ = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false),
                    PLANO_PACOTE_VIDAS = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    PLANO_VIDAS = table.Column<int>(type: "INT", nullable: false),
                    PLANO_VIDAS_ATIVADAS = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_CLIENTE", x => x.CD_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "AWM_DOCUMENTO_COMPLEMENTAR",
                schema: "dbo",
                columns: table => new
                {
                    CD_DOCUMENTACO_COMPLEMENTAR = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_DOCUMENTOCOMPLEMENTAR_ID = table.Column<string>(type: "VARCHAR(6)", nullable: true),
                    NOME = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    ORIGEM = table.Column<int>(type: "INT", nullable: false),
                    STATUS = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_DOCUMENTO_COMPLEMENTAR", x => x.CD_DOCUMENTACO_COMPLEMENTAR);
                });

            migrationBuilder.CreateTable(
                name: "AWM_HISTORICO_EXECUCAO",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DT_EXECUCAO = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_HISTORICO_EXECUCAO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AWM_USUARIO",
                schema: "dbo",
                columns: table => new
                {
                    CD_USUARIO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIPOPERFIL = table.Column<int>(type: "INT", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DOCUMENTO = table.Column<string>(type: "VARCHAR(11)", nullable: false),
                    LOGIN = table.Column<string>(type: "VARCHAR(15)", nullable: true),
                    SENHA = table.Column<string>(type: "VARCHAR(100)", maxLength: 15, nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    TELEFONE = table.Column<string>(type: "VARCHAR(16)", maxLength: 16, nullable: false),
                    MUDARSENHA = table.Column<bool>(type: "bit", nullable: false),
                    STATUS = table.Column<bool>(type: "bit", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CD_USUARIO_CRIACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    CD_USUARIO_ATUALIZACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    CD_EMPERSA = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_USUARIO", x => x.CD_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "AWM_DOCUMENTO_CARGO",
                schema: "dbo",
                columns: table => new
                {
                    CD = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_CARGO_ID = table.Column<int>(type: "INT", nullable: false),
                    CD_DOCUMENTO_ID = table.Column<int>(type: "INT", nullable: false),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_DOCUMENTO_CARGO", x => x.CD);
                    table.ForeignKey(
                        name: "FK_AWM_DOCUMENTO_CARGO_AWM_CARGO_CD_CARGO_ID",
                        column: x => x.CD_CARGO_ID,
                        principalSchema: "dbo",
                        principalTable: "AWM_CARGO",
                        principalColumn: "CD_CARGO");
                });

            migrationBuilder.CreateTable(
                name: "AWM_EMPRESA",
                schema: "dbo",
                columns: table => new
                {
                    CD_EMPRESA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    EQUITY = table.Column<int>(type: "INT", nullable: false),
                    NOMEFANTASIA = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    EMAIL = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    TELEFONE = table.Column<string>(type: "VARCHAR(15)", nullable: false),
                    STATUS = table.Column<bool>(type: "bit", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "DATE", nullable: false),
                    CD_USUARIO_CRIACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "DATE", nullable: true),
                    CD_USUARIO_ATUALIZACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    COMPLEMENTO = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    CD_CLIENTE_ID = table.Column<int>(type: "INT", nullable: false),
                    DOCUMENTACAO_VALIDADA = table.Column<bool>(type: "BIT", nullable: false),
                    ID_USUARIO_CRIACAO = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_EMPRESA", x => x.CD_EMPRESA);
                    table.ForeignKey(
                        name: "FK_AWM_EMPRESA_AWM_CLIENTE_CD_CLIENTE_ID",
                        column: x => x.CD_CLIENTE_ID,
                        principalSchema: "dbo",
                        principalTable: "AWM_CLIENTE",
                        principalColumn: "CD_CLIENTE");
                });

            migrationBuilder.CreateTable(
                name: "AWM_FUNCIONARIO",
                schema: "dbo",
                columns: table => new
                {
                    CD_FUNCIONARIO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_EMPRESA = table.Column<int>(type: "INT", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    NASCIMENTO = table.Column<DateTime>(type: "DATE", nullable: false),
                    DOCUMENTO = table.Column<string>(type: "VARCHAR(15)", nullable: false),
                    SEXO = table.Column<string>(type: "VARCHAR(1)", nullable: false),
                    ESTRANGEIRO = table.Column<bool>(type: "BIT", nullable: false),
                    TELEFONE = table.Column<string>(type: "VARCHAR(15)", nullable: true),
                    ESCOLARIDADE = table.Column<int>(type: "INT", nullable: false),
                    TIPOCONTRATO = table.Column<int>(type: "INT", nullable: false),
                    CARGO = table.Column<int>(type: "INT", nullable: false),
                    PCD = table.Column<bool>(type: "bit", nullable: false),
                    DT_ADMISSAO = table.Column<DateTime>(type: "DATE", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "DATE", nullable: false),
                    CD_USUARIO_CRIACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "DATE", nullable: true),
                    CD_USUARIO_ATUALIZACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    STATUS = table.Column<bool>(type: "bit", nullable: false),
                    INTEGRADO = table.Column<bool>(type: "bit", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    FOTO = table.Column<byte[]>(type: "BINARY", nullable: true),
                    ID_USUARIO_CRIACAO = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_FUNCIONARIO", x => x.CD_FUNCIONARIO);
                    table.ForeignKey(
                        name: "FK_AWM_FUNCIONARIO_AWM_EMPRESA_ID_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalSchema: "dbo",
                        principalTable: "AWM_EMPRESA",
                        principalColumn: "CD_EMPRESA");
                });

            migrationBuilder.CreateTable(
                name: "AWM_OBRA",
                schema: "dbo",
                columns: table => new
                {
                    CD_OBRA = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_EMPRESA_ID = table.Column<int>(type: "INT", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    STATUS = table.Column<bool>(type: "bit", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CD_USUARIO_CRIACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    CD_USUARIO_ATUALIZACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    CD_CLIENTE = table.Column<int>(type: "INT", nullable: false),
                    ID_USUARIO_CRIACAO = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_OBRA", x => x.CD_OBRA);
                    table.ForeignKey(
                        name: "FK_AWM_OBRA_AWM_EMPRESA_CD_EMPRESA_ID",
                        column: x => x.CD_EMPRESA_ID,
                        principalSchema: "dbo",
                        principalTable: "AWM_EMPRESA",
                        principalColumn: "CD_EMPRESA");
                });

            migrationBuilder.CreateTable(
                name: "AWM_ANEXO",
                schema: "dbo",
                columns: table => new
                {
                    CD_ANEXO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_EMPRESA_ID = table.Column<int>(type: "INT", nullable: true),
                    CD_FUNCIONARIO_ID = table.Column<int>(type: "INT", nullable: true),
                    NOME = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    DESCRICAO = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    ANEXO = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true),
                    DT_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DT_VALIDADE_DOCUMENTO = table.Column<DateTime>(type: "DATE", nullable: false),
                    CD_USUARIO_CRIACAO = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    ID_USUARIO_CRIACAO = table.Column<int>(type: "INT", nullable: false),
                    CD_USUARIO_ANALISTA = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
                    MOTIVO_REJEICAO = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    MOTIVO_RESALVA = table.Column<string>(type: "VARCHAR(200)", nullable: true),
                    TIPO_ANEXO = table.Column<int>(type: "INT", nullable: false),
                    STATUS = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_ANEXO", x => x.CD_ANEXO);
                    table.ForeignKey(
                        name: "FK_AWM_ANEXO_AWM_EMPRESA_CD_EMPRESA_ID",
                        column: x => x.CD_EMPRESA_ID,
                        principalSchema: "dbo",
                        principalTable: "AWM_EMPRESA",
                        principalColumn: "CD_EMPRESA");
                    table.ForeignKey(
                        name: "FK_AWM_ANEXO_AWM_FUNCIONARIO_CD_FUNCIONARIO_ID",
                        column: x => x.CD_FUNCIONARIO_ID,
                        principalSchema: "dbo",
                        principalTable: "AWM_FUNCIONARIO",
                        principalColumn: "CD_FUNCIONARIO");
                });

            migrationBuilder.CreateTable(
                name: "AWM_FUNCIONARIO_VINCULO_OBRA",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CD_FUNCIONARIO_ID = table.Column<int>(type: "int", nullable: false),
                    CD_EMPRESA_ID = table.Column<int>(type: "int", nullable: false),
                    CD_OBRA_ID = table.Column<int>(type: "int", nullable: false),
                    VINCULADO = table.Column<bool>(type: "bit", nullable: false),
                    DT_VINCULADO = table.Column<DateTime>(type: "DATE", nullable: true),
                    DT_DESVINCULADO = table.Column<DateTime>(type: "DATE", nullable: true),
                    CD_USUARIO_CRIACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    CD_USUARIO_ATUALIZACAO = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    CD_CLIENTE = table.Column<int>(type: "INT", nullable: false),
                    ID_USUARIO_CRIACAO = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AWM_FUNCIONARIO_VINCULO_OBRA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AWM_FUNCIONARIO_VINCULO_OBRA_AWM_FUNCIONARIO_CD_FUNCIONARIO_ID",
                        column: x => x.CD_FUNCIONARIO_ID,
                        principalSchema: "dbo",
                        principalTable: "AWM_FUNCIONARIO",
                        principalColumn: "CD_FUNCIONARIO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AWM_ANEXO_CD_EMPRESA_ID",
                schema: "dbo",
                table: "AWM_ANEXO",
                column: "CD_EMPRESA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AWM_ANEXO_CD_FUNCIONARIO_ID",
                schema: "dbo",
                table: "AWM_ANEXO",
                column: "CD_FUNCIONARIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AWM_DOCUMENTO_CARGO_CD_CARGO_ID",
                schema: "dbo",
                table: "AWM_DOCUMENTO_CARGO",
                column: "CD_CARGO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AWM_EMPRESA_CD_CLIENTE_ID",
                schema: "dbo",
                table: "AWM_EMPRESA",
                column: "CD_CLIENTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AWM_FUNCIONARIO_ID_EMPRESA",
                schema: "dbo",
                table: "AWM_FUNCIONARIO",
                column: "ID_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_AWM_FUNCIONARIO_VINCULO_OBRA_CD_FUNCIONARIO_ID",
                schema: "dbo",
                table: "AWM_FUNCIONARIO_VINCULO_OBRA",
                column: "CD_FUNCIONARIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AWM_OBRA_CD_EMPRESA_ID",
                schema: "dbo",
                table: "AWM_OBRA",
                column: "CD_EMPRESA_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AWM_ANEXO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_DOCUMENTO_CARGO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_DOCUMENTO_COMPLEMENTAR",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_FUNCIONARIO_VINCULO_OBRA",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_HISTORICO_EXECUCAO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_OBRA",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_USUARIO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_CARGO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_FUNCIONARIO",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_EMPRESA",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AWM_CLIENTE",
                schema: "dbo");
        }
    }
}
