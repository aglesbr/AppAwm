﻿// <auto-generated />
using System;
using AppAwm.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppAwm.Migrations
{
    [DbContext(typeof(DbCon))]
    [Migration("20241230211503_secondV02")]
    partial class secondV02
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppAwm.Models.Anexo", b =>
                {
                    b.Property<int>("Cd_Anexo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD_ANEXO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cd_Anexo"));

                    b.Property<byte[]>("Arquivo")
                        .HasColumnType("VARBINARY(MAX)")
                        .HasColumnName("ANEXO");

                    b.Property<int?>("Cd_Empresa_Id")
                        .HasColumnType("INT")
                        .HasColumnName("CD_EMPRESA_ID");

                    b.Property<int?>("Cd_Funcionario_Id")
                        .HasColumnType("INT")
                        .HasColumnName("CD_FUNCIONARIO_ID");

                    b.Property<string>("Cd_UsuarioAnalista")
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("CD_USUARIO_ANALISTA");

                    b.Property<string>("Cd_UsuarioCriacao")
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("CD_USUARIO_CRIACAO");

                    b.Property<string>("Descricao")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("DESCRICAO");

                    b.Property<DateTime>("Dt_Criacao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DT_CRIACAO");

                    b.Property<DateTime>("Dt_Validade_Documento")
                        .HasColumnType("DATE")
                        .HasColumnName("DT_VALIDADE_DOCUMENTO");

                    b.Property<int>("Id_UsuarioCriacao")
                        .HasColumnType("INT")
                        .HasColumnName("ID_USUARIO_CRIACAO");

                    b.Property<string>("MotivoRejeicao")
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("MOTIVO_REJEICAO");

                    b.Property<string>("MotivoResalva")
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("MOTIVO_RESALVA");

                    b.Property<string>("Nome")
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("NOME");

                    b.Property<int?>("Status")
                        .HasColumnType("INT")
                        .HasColumnName("STATUS");

                    b.Property<int>("TipoAnexo")
                        .HasColumnType("INT")
                        .HasColumnName("TIPO_ANEXO");

                    b.HasKey("Cd_Anexo");

                    b.HasIndex("Cd_Empresa_Id");

                    b.HasIndex("Cd_Funcionario_Id");

                    b.ToTable("AWM_ANEXO", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.Cargo", b =>
                {
                    b.Property<int>("Cd_Cargo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD_CARGO")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cd_Cargo"));

                    b.Property<string>("Nome")
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("NOME")
                        .HasColumnOrder(5);

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("STATUS")
                        .HasColumnOrder(10);

                    b.HasKey("Cd_Cargo");

                    b.ToTable("AWM_CARGO", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.Cliente", b =>
                {
                    b.Property<int>("Cd_Cliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD_CLIENTE")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cd_Cliente"));

                    b.Property<string>("CanalMq")
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CANALMQ")
                        .HasColumnOrder(21);

                    b.Property<string>("HostMq")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("HOSTMQ")
                        .HasColumnOrder(20);

                    b.Property<string>("Nome")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("NOME")
                        .HasColumnOrder(5);

                    b.Property<string>("PasswordMq")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("PASSWORDMQ")
                        .HasColumnOrder(15);

                    b.Property<string>("PlanoPacoteVidas")
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("PLANO_PACOTE_VIDAS")
                        .HasColumnOrder(26);

                    b.Property<int>("PlanoVidas")
                        .HasColumnType("INT")
                        .HasColumnName("PLANO_VIDAS")
                        .HasColumnOrder(27);

                    b.Property<int>("PlanoVidasAtivadas")
                        .HasColumnType("INT")
                        .HasColumnName("PLANO_VIDAS_ATIVADAS")
                        .HasColumnOrder(28);

                    b.Property<bool>("Status")
                        .HasColumnType("BIT")
                        .HasColumnName("STATUS")
                        .HasColumnOrder(25);

                    b.Property<string>("UsuarioMq")
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("USUARIOMQ")
                        .HasColumnOrder(10);

                    b.HasKey("Cd_Cliente");

                    b.ToTable("AWM_CLIENTE", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.Colaborador", b =>
                {
                    b.Property<int>("Cd_Funcionario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD_FUNCIONARIO")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cd_Funcionario"));

                    b.Property<int>("Cd_Cargo")
                        .HasColumnType("INT")
                        .HasColumnName("CARGO")
                        .HasColumnOrder(40);

                    b.Property<string>("Cd_UsuarioAtualizacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_ATUALIZACAO")
                        .HasColumnOrder(75);

                    b.Property<string>("Cd_UsuarioCriacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_CRIACAO")
                        .HasColumnOrder(65);

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("VARCHAR(15)")
                        .HasColumnName("DOCUMENTO")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("Dt_Admissao")
                        .HasColumnType("DATE")
                        .HasColumnName("DT_ADMISSAO")
                        .HasColumnOrder(55);

                    b.Property<DateTime?>("Dt_Atualizacao")
                        .HasColumnType("DATE")
                        .HasColumnName("DT_ATUALIZACAO")
                        .HasColumnOrder(70);

                    b.Property<DateTime>("Dt_Criacao")
                        .HasColumnType("DATE")
                        .HasColumnName("DT_CRIACAO")
                        .HasColumnOrder(60);

                    b.Property<int>("Escolaridade")
                        .HasColumnType("INT")
                        .HasColumnName("ESCOLARIDADE")
                        .HasColumnOrder(30);

                    b.Property<bool>("Estrangeiro")
                        .HasColumnType("BIT")
                        .HasColumnName("ESTRANGEIRO")
                        .HasColumnOrder(16);

                    b.Property<byte[]>("Foto")
                        .HasColumnType("BINARY")
                        .HasColumnName("FOTO")
                        .HasColumnOrder(90);

                    b.Property<int>("Id_Empresa")
                        .HasColumnType("INT")
                        .HasColumnName("ID_EMPRESA")
                        .HasColumnOrder(2);

                    b.Property<int>("Id_UsuarioCriacao")
                        .HasColumnType("INT")
                        .HasColumnName("ID_USUARIO_CRIACAO")
                        .HasColumnOrder(100);

                    b.Property<bool>("Integrado")
                        .HasColumnType("bit")
                        .HasColumnName("INTEGRADO")
                        .HasColumnOrder(81);

                    b.Property<DateTime>("Nascimento")
                        .HasColumnType("DATE")
                        .HasColumnName("NASCIMENTO")
                        .HasColumnOrder(4);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("NOME")
                        .HasColumnOrder(3);

                    b.Property<string>("Observacao")
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("OBSERVACAO")
                        .HasColumnOrder(85);

                    b.Property<bool>("Pcd")
                        .HasColumnType("bit")
                        .HasColumnName("PCD")
                        .HasColumnOrder(46);

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("VARCHAR(1)")
                        .HasColumnName("SEXO")
                        .HasColumnOrder(15);

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("STATUS")
                        .HasColumnOrder(80);

                    b.Property<string>("Telefone")
                        .HasColumnType("VARCHAR(15)")
                        .HasColumnName("TELEFONE")
                        .HasColumnOrder(25);

                    b.Property<int>("TipoContrato")
                        .HasColumnType("INT")
                        .HasColumnName("TIPOCONTRATO")
                        .HasColumnOrder(35);

                    b.HasKey("Cd_Funcionario");

                    b.HasIndex("Id_Empresa");

                    b.ToTable("AWM_FUNCIONARIO", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.ColaboradorVinculoObra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cd_Cliente")
                        .HasColumnType("INT")
                        .HasColumnName("CD_CLIENTE")
                        .HasColumnOrder(95);

                    b.Property<int>("Cd_Empresa_Id")
                        .HasColumnType("int")
                        .HasColumnName("CD_EMPRESA_ID")
                        .HasColumnOrder(10);

                    b.Property<int>("Cd_Funcionario_Id")
                        .HasColumnType("int")
                        .HasColumnName("CD_FUNCIONARIO_ID")
                        .HasColumnOrder(5);

                    b.Property<int>("Cd_Obra_Id")
                        .HasColumnType("int")
                        .HasColumnName("CD_OBRA_ID")
                        .HasColumnOrder(15);

                    b.Property<string>("Cd_UsuarioAtualizacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_ATUALIZACAO")
                        .HasColumnOrder(40);

                    b.Property<string>("Cd_UsuarioCriacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_CRIACAO")
                        .HasColumnOrder(35);

                    b.Property<DateTime?>("DataDesvinculado")
                        .HasColumnType("DATE")
                        .HasColumnName("DT_DESVINCULADO")
                        .HasColumnOrder(30);

                    b.Property<DateTime?>("DataVinculado")
                        .HasColumnType("DATE")
                        .HasColumnName("DT_VINCULADO")
                        .HasColumnOrder(25);

                    b.Property<int>("Id_UsuarioCriacao")
                        .HasColumnType("INT")
                        .HasColumnName("ID_USUARIO_CRIACAO")
                        .HasColumnOrder(100);

                    b.Property<bool>("Vinculado")
                        .HasColumnType("bit")
                        .HasColumnName("VINCULADO")
                        .HasColumnOrder(20);

                    b.HasKey("Id");

                    b.HasIndex("Cd_Funcionario_Id");

                    b.ToTable("AWM_FUNCIONARIO_VINCULO_OBRA", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.DocumentacaoCargo", b =>
                {
                    b.Property<int>("Cd")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cd"));

                    b.Property<int>("Cd_Cargo_Id")
                        .HasColumnType("INT")
                        .HasColumnName("CD_CARGO_ID")
                        .HasColumnOrder(5);

                    b.Property<int>("Cd_Documento_Id")
                        .HasColumnType("INT")
                        .HasColumnName("CD_DOCUMENTO_ID")
                        .HasColumnOrder(10);

                    b.Property<bool>("Status")
                        .HasColumnType("BIT")
                        .HasColumnName("STATUS")
                        .HasColumnOrder(15);

                    b.HasKey("Cd");

                    b.HasIndex("Cd_Cargo_Id");

                    b.ToTable("AWM_DOCUMENTO_CARGO", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.DocumentacaoComplementar", b =>
                {
                    b.Property<int>("Cd_Documentaco_Complementar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD_DOCUMENTACO_COMPLEMENTAR")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cd_Documentaco_Complementar"));

                    b.Property<string>("Cd_DocumentoComplementar_Id")
                        .HasColumnType("VARCHAR(6)")
                        .HasColumnName("CD_DOCUMENTOCOMPLEMENTAR_ID")
                        .HasColumnOrder(5);

                    b.Property<string>("Nome")
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("NOME")
                        .HasColumnOrder(10);

                    b.Property<int>("Origem")
                        .HasColumnType("INT")
                        .HasColumnName("ORIGEM")
                        .HasColumnOrder(11);

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("STATUS")
                        .HasColumnOrder(15);

                    b.HasKey("Cd_Documentaco_Complementar");

                    b.ToTable("AWM_DOCUMENTO_COMPLEMENTAR", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.Empresa", b =>
                {
                    b.Property<int>("Cd_Empresa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CD_EMPRESA")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cd_Empresa"));

                    b.Property<int>("Cd_Cliente_Id")
                        .HasColumnType("INT")
                        .HasColumnName("CD_CLIENTE_ID")
                        .HasColumnOrder(16);

                    b.Property<string>("Cd_UsuarioAtualizacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_ATUALIZACAO")
                        .HasColumnOrder(13);

                    b.Property<string>("Cd_UsuarioCriacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_CRIACAO")
                        .HasColumnOrder(11);

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("VARCHAR(14)")
                        .HasColumnName("CNPJ")
                        .HasColumnOrder(2);

                    b.Property<string>("Complemento")
                        .HasColumnType("VARCHAR(5000)")
                        .HasColumnName("COMPLEMENTO")
                        .HasColumnOrder(14);

                    b.Property<bool>("DocumentacaoValidada")
                        .HasColumnType("BIT")
                        .HasColumnName("DOCUMENTACAO_VALIDADA")
                        .HasColumnOrder(17);

                    b.Property<DateTime?>("Dt_Atualizacao")
                        .HasColumnType("DATE")
                        .HasColumnName("DT_ATUALIZACAO")
                        .HasColumnOrder(12);

                    b.Property<DateTime>("Dt_Criacao")
                        .HasColumnType("DATE")
                        .HasColumnName("DT_CRIACAO")
                        .HasColumnOrder(10);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("EMAIL")
                        .HasColumnOrder(6);

                    b.Property<int>("Equity")
                        .HasColumnType("INT")
                        .HasColumnName("EQUITY")
                        .HasColumnOrder(4);

                    b.Property<int>("Id_UsuarioCriacao")
                        .HasColumnType("INT")
                        .HasColumnName("ID_USUARIO_CRIACAO")
                        .HasColumnOrder(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("NOME")
                        .HasColumnOrder(3);

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("NOMEFANTASIA")
                        .HasColumnOrder(5);

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("STATUS")
                        .HasColumnOrder(9);

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("VARCHAR(15)")
                        .HasColumnName("TELEFONE")
                        .HasColumnOrder(8);

                    b.HasKey("Cd_Empresa");

                    b.HasIndex("Cd_Cliente_Id");

                    b.ToTable("AWM_EMPRESA", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.HistoricoExecucao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("ID")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Dt_Execucao")
                        .HasColumnType("DATE")
                        .HasColumnName("DT_EXECUCAO")
                        .HasColumnOrder(10);

                    b.HasKey("Id");

                    b.ToTable("AWM_HISTORICO_EXECUCAO", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.Obra", b =>
                {
                    b.Property<int>("Cd_Obra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD_OBRA")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cd_Obra"));

                    b.Property<int>("Cd_Cliente")
                        .HasColumnType("INT")
                        .HasColumnName("CD_CLIENTE")
                        .HasColumnOrder(95);

                    b.Property<int>("Cd_Empresa_Id")
                        .HasColumnType("INT")
                        .HasColumnName("CD_EMPRESA_ID")
                        .HasColumnOrder(5);

                    b.Property<string>("Cd_Usuario_Atualizacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_ATUALIZACAO")
                        .HasColumnOrder(40);

                    b.Property<string>("Cd_Usuario_Criacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_CRIACAO")
                        .HasColumnOrder(30);

                    b.Property<string>("Descricao")
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("DESCRICAO")
                        .HasColumnOrder(15);

                    b.Property<DateTime?>("Dt_Atualizacao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DT_ATUALIZACAO")
                        .HasColumnOrder(35);

                    b.Property<DateTime>("Dt_Criacao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DT_CRIACAO")
                        .HasColumnOrder(25);

                    b.Property<int>("Id_UsuarioCriacao")
                        .HasColumnType("INT")
                        .HasColumnName("ID_USUARIO_CRIACAO")
                        .HasColumnOrder(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("NOME")
                        .HasColumnOrder(10);

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("STATUS")
                        .HasColumnOrder(20);

                    b.HasKey("Cd_Obra");

                    b.HasIndex("Cd_Empresa_Id");

                    b.ToTable("AWM_OBRA", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.Usuario", b =>
                {
                    b.Property<int>("Cd_Usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("CD_USUARIO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cd_Usuario"));

                    b.Property<int>("Cd_Empresa")
                        .HasColumnType("INT")
                        .HasColumnName("CD_EMPERSA");

                    b.Property<string>("Cd_Usuario_Atualizacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_ATUALIZACAO");

                    b.Property<string>("Cd_Usuario_Criacao")
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("CD_USUARIO_CRIACAO");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("VARCHAR(11)")
                        .HasColumnName("DOCUMENTO");

                    b.Property<DateTime?>("Dt_Atualizacao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DT_ATUALIZACAO");

                    b.Property<DateTime>("Dt_Criacao")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DT_CRIACAO");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Login")
                        .HasColumnType("VARCHAR(15)")
                        .HasColumnName("LOGIN");

                    b.Property<bool>("MudarSenha")
                        .HasColumnType("bit")
                        .HasColumnName("MUDARSENHA");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("NOME");

                    b.Property<int>("Perfil")
                        .HasColumnType("INT")
                        .HasColumnName("TIPOPERFIL");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("SENHA");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("STATUS");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("VARCHAR(16)")
                        .HasColumnName("TELEFONE");

                    b.HasKey("Cd_Usuario");

                    b.ToTable("AWM_USUARIO", "dbo");
                });

            modelBuilder.Entity("AppAwm.Models.Anexo", b =>
                {
                    b.HasOne("AppAwm.Models.Empresa", "Empresa")
                        .WithMany("Anexos")
                        .HasForeignKey("Cd_Empresa_Id");

                    b.HasOne("AppAwm.Models.Colaborador", "Colaborador")
                        .WithMany("Anexos")
                        .HasForeignKey("Cd_Funcionario_Id");

                    b.Navigation("Colaborador");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("AppAwm.Models.Colaborador", b =>
                {
                    b.HasOne("AppAwm.Models.Empresa", "Empresa")
                        .WithMany("Funcionarios")
                        .HasForeignKey("Id_Empresa");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("AppAwm.Models.ColaboradorVinculoObra", b =>
                {
                    b.HasOne("AppAwm.Models.Colaborador", "Colaborador")
                        .WithMany("VinculoObras")
                        .HasForeignKey("Cd_Funcionario_Id");

                    b.Navigation("Colaborador");
                });

            modelBuilder.Entity("AppAwm.Models.DocumentacaoCargo", b =>
                {
                    b.HasOne("AppAwm.Models.Cargo", "Cargo")
                        .WithMany("DocumentoComplementar")
                        .HasForeignKey("Cd_Cargo_Id");

                    b.Navigation("Cargo");
                });

            modelBuilder.Entity("AppAwm.Models.Empresa", b =>
                {
                    b.HasOne("AppAwm.Models.Cliente", "Cliente")
                        .WithMany("Empresas")
                        .HasForeignKey("Cd_Cliente_Id");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("AppAwm.Models.Obra", b =>
                {
                    b.HasOne("AppAwm.Models.Empresa", "Empresa")
                        .WithMany("Obras")
                        .HasForeignKey("Cd_Empresa_Id");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("AppAwm.Models.Cargo", b =>
                {
                    b.Navigation("DocumentoComplementar");
                });

            modelBuilder.Entity("AppAwm.Models.Cliente", b =>
                {
                    b.Navigation("Empresas");
                });

            modelBuilder.Entity("AppAwm.Models.Colaborador", b =>
                {
                    b.Navigation("Anexos");

                    b.Navigation("VinculoObras");
                });

            modelBuilder.Entity("AppAwm.Models.Empresa", b =>
                {
                    b.Navigation("Anexos");

                    b.Navigation("Funcionarios");

                    b.Navigation("Obras");
                });
#pragma warning restore 612, 618
        }
    }
}
