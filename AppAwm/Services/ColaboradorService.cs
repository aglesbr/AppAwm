﻿using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using AppAwm.Util;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class ColaboradorService : IColaborador<ColaboradorAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public bool Check(Expression<Func<Colaborador, bool>> predicate)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Colaborador>(db, out status);

            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                return contexto.GetItem(predicate) is not null;

            return false;
        }

        public ColaboradorAnswer Get(Expression<Func<Colaborador, bool>> predicate, Usuario? usuario)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    Colaborador? funcionario = contexto.GetAll(predicate).FirstOrDefault();

                    if (funcionario != null)
                    {
                        var contextoCargo = new RepositoryGeneric<Cargo>(db, out status);
                        funcionario.Cargo = contextoCargo.GetItem(s => s.Cd_Cargo == funcionario.Cd_Cargo);

                    }
                    // consome lista de empresas
                    using var contextoEmpresa = new RepositoryGeneric<Empresa>(db, out status);
                    List<SelectListItem> empresas = [.. contextoEmpresa.GetAll(p =>
                     (usuario!.Perfil == EnumPerfil.Administrador ? p.Cd_Empresa > 0 : p.Cd_Empresa == usuario.Cd_Empresa)
                     && p.Status
                    ).
                    Select(p => new SelectListItem { Value = p.Cd_Empresa.ToString(), Text = p.Nome })];


                    return ColaboradorAnswer.Bind(funcionario, empresas);
                }

                return ColaboradorAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return ColaboradorAnswer.DeErro(ex.Message);
            }
        }

        public ColaboradorAnswer List(Expression<Func<Colaborador, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Colaborador> funcionarios = [.. contexto.GetAll(predicate).Include(emp => emp.Empresa).OrderBy(o => o.Nome)
                        .Include(o => o.VinculoObras)
                        .Include(a => a.Anexos)
                        .Select(ss => new Colaborador 
                        { 
                             Anexos = (ICollection<Anexo>)ss.Anexos.Select(anx =>  
                             new Anexo { 
                                 Cd_Anexo = anx.Cd_Anexo, TipoAnexo = anx.TipoAnexo, Status = anx.Status, Cd_Empresa_Id = anx.Cd_Empresa_Id, Cd_Funcionario_Id = anx.Cd_Funcionario_Id,
                                  MotivoRejeicao = anx.MotivoRejeicao, MotivoResalva = anx.MotivoResalva, Nome = anx.Nome
                             }),
                             Cd_Funcionario = ss.Cd_Funcionario,
                             Documento = ss.Documento,
                             Integrado = ss.Integrado,
                             Cd_UsuarioCriacao = ss.Cd_UsuarioCriacao,
                             Status = ss.Status,
                             Nascimento = ss.Nascimento,
                             Escolaridade = ss.Escolaridade,
                             Id_UsuarioCriacao = ss.Id_UsuarioCriacao,
                             Foto = ss.Foto,
                             Estrangeiro = ss.Estrangeiro,
                             Pcd = ss.Pcd,
                             Sexo = ss.Sexo,
                             TipoContrato = ss.TipoContrato,
                             Id_Empresa = ss.Id_Empresa,
                             Cd_Cargo = ss.Cd_Cargo,
                             Nome = ss.Nome,
                             Telefone = ss.Telefone
                        })];


                    return funcionarios.Count > 0 ? ColaboradorAnswer.DeSucesso(funcionarios) : ColaboradorAnswer.DeErro("Nenhum registro fui localizado");
                }

                return ColaboradorAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return ColaboradorAnswer.DeErro(ex.Message);
            }
        }

        public ColaboradorAnswer Save(Colaborador funcionario, EnumAcao acao)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Colaborador>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    if (acao == EnumAcao.Editar)
                    {
                        var obj = contexto.GetItem(x => x.Cd_Funcionario == funcionario.Cd_Funcionario) ?? new();
                        funcionario.Foto = obj.Foto!;
                    }
                    int ret = acao == EnumAcao.Criar ? contexto.Create(funcionario) : contexto.Edit(funcionario);

                    return ret > 0 ? ColaboradorAnswer.DeSucesso(acao) : ColaboradorAnswer.DeErro("Ocorreu um erro ao tentar registar a empresa"); ;
                }

                return ColaboradorAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return ColaboradorAnswer.DeErro(ex.Message);
            }
        }

        public List<Cargo> GetCargos(string nome)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Cargo>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    return [.. contexto.GetAll(g => g.Nome!.ToLower().StartsWith(nome.ToLower())).Take(50)];
                }

                return [];

            }
            catch
            {
                throw;
            }
        }

        public List<Empresa> GetEmpresas(Expression<Func<Empresa, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Empresa>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    return [.. contexto.GetAll(predicate)];
                }

                return [];

            }
            catch
            {
                throw;
            }
        }

        public int UpdateFoto(Colaborador funcionario)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);

                var resposta = db.Funcionarios.Where(w => w.Cd_Funcionario == funcionario.Cd_Funcionario)
                .ExecuteUpdate(ax => ax.SetProperty(sp => sp.Foto, funcionario.Foto));

                return resposta;
            }
            catch
            {
                return 0;
            }
        }

        public Cracha? GetCracha(int? id = 0)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);


                var resposta = db.Funcionarios.Where(w => w.Cd_Funcionario == id)
                    .Include(emp => emp.Empresa).FirstOrDefault();

                if (resposta != null)
                {
                    var cargo = db.Cargos.SingleOrDefault(s => s.Cd_Cargo == resposta.Cd_Cargo);

                    Cracha cracha = new() { Nome = resposta.Nome, Documento = resposta.Documento, Empresa = resposta.Empresa.Nome, Foto = resposta.Foto, Cargo = cargo.Nome, QrCode = null };

                    return cracha;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public int UpdateCliente(bool isAdd)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Cliente>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    if (isAdd)
                        Utility.Cliente.PlanoVidasAtivadas++;
                    else
                        Utility.Cliente.PlanoVidasAtivadas--;

                    int ret = contexto.Edit(Utility.Cliente);

                    return ret;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public ColaboradorAnswer ImportarColaboradore(MemoryStream stream, Usuario? usuario, int cd_empresa)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Colaborador>(db, out status);

                Colaborador? colaborador = null;
                List<Colaborador> list = [];

                string[] header = ["Colaborador", "Sexo", "Função", "CPF", "Telefone", "Data_nascimento", "Data_admissão"];
                bool validaArquivo = true;
                int linha = 0, fimLista = 0;
                object campo;

                List<Colaborador> checkList = db.Funcionarios.Where(w => w.Id_Empresa == cd_empresa).ToList();

                Empresa? empresa = GetEmpresas(emp => emp.Status && emp.Cd_Empresa == cd_empresa).FirstOrDefault();

                if (empresa == null)
                    return ColaboradorAnswer.DeErro($"A empresa com o cnpj: {empresa!.Cnpj}, ainda não está cadastrada no sistema.");

                using var reader = ExcelReaderFactory.CreateReader(stream);
                
                do
                {
                    while (reader.Read())
                    {

                        if (linha++ == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                campo = reader.GetValue(i);

                                if (campo != null)
                                {
                                    if (!header.Contains(campo.ToString()))
                                    {
                                        validaArquivo = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    validaArquivo = false;
                                    break;
                                }
                            }
                        }

                        if (linha > 1) break;
                    }

                    if (!validaArquivo)
                        return ColaboradorAnswer.DeErro("O arquivo enviado para importação não é válido");
                    else
                        break;
                } while (reader.Read());

                linha = 0;

                using var readerData = ExcelReaderFactory.CreateReader(stream);

                do
                {
                    while (readerData.Read())
                    {
                        if (linha++ == 0) continue;

                        if (!validaArquivo) break;

                        colaborador = new()
                        {
                            Id_Empresa = cd_empresa,
                            Id_UsuarioCriacao = usuario.Cd_Usuario,
                            Escolaridade = 2,
                            Status = true,
                            Cd_Cargo = 1,
                            Cd_UsuarioCriacao = usuario.Nome,
                            Integrado = false,
                            TipoContrato = 1,
                            Dt_Criacao = DateTime.Now.Date
                        };

                        for (int coluna = 0; coluna < readerData.FieldCount; coluna++)
                        {
                            campo = readerData.GetValue(coluna);

                            if (fimLista > 3)
                                break;

                            if (campo == null) { fimLista++; continue; }

                            if (coluna == 0)
                                colaborador.Nome = campo.ToString();
                            if (coluna == 1)
                                colaborador.Sexo = campo.ToString();
                            if (coluna == 2)
                                colaborador.Cd_Cargo = Convert.ToInt32(campo);
                            if (coluna == 3)
                                colaborador.Documento = campo.ToString();
                            if (coluna == 4)
                                colaborador.Telefone = campo.ToString();
                            if (coluna == 5)
                                colaborador.Nascimento = Convert.ToDateTime(campo.ToString().Replace('.', '/'));
                            if (coluna == 6)
                                colaborador.Dt_Admissao = Convert.ToDateTime(campo.ToString().Replace('.', '/'));
                        }

                        if (fimLista < 3)
                        {
                            if (checkList.Any(n => n.Documento == colaborador.Documento)) continue;

                            list.Add(colaborador);
                        }
                        else
                            break;
                    }
                    
                } while (readerData.NextResult());

                int retorno = contexto.BulkInsert(list);

                return retorno > 0 ? ColaboradorAnswer.DeSucesso(EnumAcao.Criar) : ColaboradorAnswer.DeErro("Todos os colaboradores da planilha<br/>já foram cadastrados anteriormente.");
            }
            catch (Exception ex)
            {
                return ColaboradorAnswer.DeErro("Ocorreu um erro na importação,<br/> verifique se dos os campos estão preenchido conforme o modelo da planilha fornecida<br/>ERRO " +ex.Message);
            }
        }
      
        public bool CheckClienteVidasDisponivel()
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Cliente>(db, out status);

            var cliente = contexto.GetItem(c => c.Cd_Cliente == Utility.Cliente!.Cd_Cliente);

            return Utility.Cliente!.PlanoVidasAtivadas < cliente!.PlanoVidas;
        }
    }
}
