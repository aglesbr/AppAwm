using AppAwm.DAL;
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
    public class FuncionarioService : IFuncionario<FuncionarioAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public bool Check(Expression<Func<Funcionario, bool>> predicate)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Funcionario>(db, out status);

            if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                return contexto.GetItem(predicate) is not null;
            
            return false;
        }

        public FuncionarioAnswer Get(Expression<Func<Funcionario, bool>> predicate, Usuario? usuario)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Funcionario>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    Funcionario? funcionario = contexto.GetAll(predicate)
                        .Include(f => f.Endereco).FirstOrDefault();

                    if (funcionario != null)
                    {
                        var contextoCargo = new RepositoryGeneric<Cargo>(db, out status);
                        funcionario.Cargo = contextoCargo.GetItem(s => s.Cd_Cargo == funcionario.Cd_Cargo);
                        
                    }
                    // consome lista de empresas
                    using var contextoEmpresa = new RepositoryGeneric<Empresa>(db, out status);
                    List<SelectListItem> empresas = [.. contextoEmpresa.GetAll(p =>
                     p.Cd_UsuarioCriacao == (usuario!.Perfil == EnumPerfil.Administrador ? p.Cd_UsuarioCriacao : usuario.Nome)
                     && p.Status
                    ).
                    Select(p => new SelectListItem { Value = p.Cd_Empresa.ToString(), Text = p.Nome })];


                    return FuncionarioAnswer.Bind(funcionario,empresas) ;
                }

                return FuncionarioAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return FuncionarioAnswer.DeErro(ex.Message);
            }
        }

        public FuncionarioAnswer List(Expression<Func<Funcionario, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Funcionario>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Funcionario> funcionarios = [.. contexto.GetAll(predicate).Include(emp => emp.Empresa).OrderBy(o => o.Nome)
                        .Include(o => o.VinculoObras)
                        .Include(a =>  a.Anexos)];

                    funcionarios.ForEach(f =>
                    {
                        if (f.Anexos.Any())
                        { 
                            f.Anexos.ToList().ForEach(a => { a.Arquivo =  null; });
                        }
                        
                    });
                    return funcionarios.Count > 0 ? FuncionarioAnswer.DeSucesso(funcionarios) : FuncionarioAnswer.DeErro("Nenhum registro fui localizado");
                }

                return FuncionarioAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return FuncionarioAnswer.DeErro(ex.Message);
            }
        }

        public FuncionarioAnswer Save(Funcionario funcionario, EnumAcao acao)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Funcionario>(db, out status);
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

                    if (acao == EnumAcao.Editar)
                    {
                        var contextoEndereco = new RepositoryGeneric<Endereco>(db, out status);
                        contextoEndereco.Edit(funcionario.Endereco);
                    }

                    return ret > 0 ? FuncionarioAnswer.DeSucesso(acao) : FuncionarioAnswer.DeErro("Ocorreu um erro ao tentar registar a empresa"); ;
                }

                return FuncionarioAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return FuncionarioAnswer.DeErro(ex.Message);
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
                    return contexto.GetAll(g =>  g.Nome!.StartsWith(nome)).Take(50).ToList();   
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

        public int UpdateFoto(Funcionario funcionario)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Funcionario>(db, out status);
                
                var resposta = db.Funcionarios.Where(w => w.Cd_Funcionario == funcionario.Cd_Funcionario)
                .ExecuteUpdate(ax => ax.SetProperty(sp=> sp.Foto, funcionario.Foto));

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
                using var contexto = new RepositoryGeneric<Funcionario>(db, out status);


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
