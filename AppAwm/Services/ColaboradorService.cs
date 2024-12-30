﻿using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using AppAwm.Util;
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
                        .Include(a =>  a.Anexos)];

                    funcionarios.ForEach(f =>
                    {
                        if (f.Anexos.Any())
                        {
                            f.Anexos.ToList().ForEach(a => { a.Arquivo = null; });
                        }

                    });
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
                    return contexto.GetAll(g => g.Nome!.StartsWith(nome)).Take(50).ToList();
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
    }
}