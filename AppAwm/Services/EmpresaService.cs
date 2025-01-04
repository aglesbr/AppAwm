using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class EmpresaService : IEmpresa<EmpresaAnswer>
    {
        public EmpresaService() { }

        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public EmpresaAnswer Get(Expression<Func<Empresa, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Empresa>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    Empresa? empresa = contexto.GetAll(predicate).Include(f => f.Funcionarios).FirstOrDefault();

                    return empresa is not null ? EmpresaAnswer.DeSucesso(empresa) : EmpresaAnswer.DeFalha("Nenhum registro fui localizado");
                }

                return EmpresaAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return EmpresaAnswer.DeFalha(ex.Message);
            }

        }

        public EmpresaAnswer List(Expression<Func<Empresa, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Empresa>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Empresa> empresas = [.. contexto.GetAll(predicate)
                        .Include(f => f.Funcionarios)
                        .Include(o => o.Obras)
                        .OrderBy(o => o.Nome)];
                    return empresas.Count > 0 ? EmpresaAnswer.DeSucesso(empresas) : EmpresaAnswer.DeFalha("Nenhum registro fui localizado");
                }

                return EmpresaAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return EmpresaAnswer.DeFalha(ex.Message);
            }
        }

        public EmpresaAnswer ListChart(Expression<Func<Empresa, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Empresa>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var empresas = contexto.GetAll(predicate).ToList().Select(s => new Empresa { Cd_Empresa = s.Cd_Empresa }).ToList();
                    return empresas.Count > 0 ? EmpresaAnswer.DeSucesso(empresas) : EmpresaAnswer.DeFalha("Nenhum registro fui localizado");
                }

                return EmpresaAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return EmpresaAnswer.DeFalha(ex.Message);
            }
        }

        public EmpresaAnswer Save(Empresa empresa, EnumAcao acao)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Empresa>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    int ret = acao == EnumAcao.Criar ? contexto.Create(empresa) : contexto.Edit(empresa);

                    if (ret <= 0)
                        return EmpresaAnswer.DeFalha("Ocorreu um erro ao tentar registar a empresa");

                    return ret > 0 ? EmpresaAnswer.DeSucesso(acao) : EmpresaAnswer.DeErro();
                }

                return EmpresaAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return EmpresaAnswer.DeFalha(ex.Message);
            }
        }

        public async ValueTask<EmpresaAnswer> GetCnpj(string cnpj)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, string.Format(Util.Utility.UrlApi, cnpj));
                request.Headers.Add("Authorization", Util.Utility.KeyApi);
                var response = await client.SendAsync(request);
                var isSuccess = response.EnsureSuccessStatusCode();

                if (isSuccess.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string valueCnpj = response.Content.ReadAsStringAsync().Result;
                    return EmpresaAnswer.DeSucesso(valueCnpj);
                }
                else
                {
                    return EmpresaAnswer.DeFalha("Ocorreu um erro ao tentar consultar o cnpj");
                }

            }
            catch
            {
                return EmpresaAnswer.DeErro("Nenhum registro foi localizado.");
            }
        }

        public int Vincular(ColaboradorVinculoObra vinculoObra)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<ColaboradorVinculoObra>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var retorno = contexto.GetItem(x => x.Cd_Cliente == vinculoObra.Cd_Cliente && x.Cd_Empresa_Id == vinculoObra.Cd_Empresa_Id && x.Cd_Funcionario_Id == vinculoObra.Cd_Funcionario_Id && x.Cd_Obra_Id == vinculoObra.Cd_Obra_Id);

                    vinculoObra.DataVinculado = retorno == null ? DateTime.Now.Date : retorno.DataVinculado;

                    if (retorno != null)
                    {
                        retorno.Cd_UsuarioAtualizacao = retorno.Cd_UsuarioCriacao;
                        retorno.DataDesvinculado = !vinculoObra.Vinculado ? DateTime.Now.Date : null;
                        retorno.Vinculado = vinculoObra.Vinculado;
                    }

                    int ret = retorno == null ? contexto.Create(vinculoObra) : contexto.Edit(retorno);

                    return ret;
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<Cliente> GetClientes(Expression<Func<Cliente, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Cliente>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Cliente> clientes = [.. contexto.GetAll(predicate)];

                    return clientes;
                }

                return new();
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        //public void OpenTransaction()
        //{
        //    using DbCon db = new();
        //    using var contexto = new RepositoryGeneric<Empresa>(db, out status);
        //    contexto.StartTransaction();
        //}

        //public void CloseTransaction()
        //{
        //    using DbCon db = new();
        //    using var contexto = new RepositoryGeneric<Empresa>(db, out status);
        //    contexto.FinishTransactio();
        //}
    }
}
