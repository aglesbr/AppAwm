using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class DepartamentoService : IDepartamento
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public bool Check(Expression<Func<Departamento, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public DepartamentoAnswer Get(Expression<Func<Departamento, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Departamento>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    Departamento? departamento = contexto.GetAll(predicate).FirstOrDefault();

                    if (departamento != null)
                    {
                       return DepartamentoAnswer.DeSucesso(departamento);
                    }
                }

                return DepartamentoAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return DepartamentoAnswer.DeErro(ex.Message);
            }
        }

        public DepartamentoAnswer List(Expression<Func<Departamento, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Departamento>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Departamento> departamentos = [.. contexto.GetAll(predicate).OrderBy(o => o.Nome)];
                    return departamentos.Count > 0 ? DepartamentoAnswer.DeSucesso(departamentos) : DepartamentoAnswer.DeErro("Nenhum registro fui localizado");
                }

                return DepartamentoAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return DepartamentoAnswer.DeErro(ex.Message);
            }
        }

        public DepartamentoAnswer Save(Departamento objetoSave, EnumAcao acao)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Departamento>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    int ret = acao == EnumAcao.Criar ? contexto.Create(objetoSave) : contexto.Edit(objetoSave);
                    return ret > 0 ? DepartamentoAnswer.DeSucesso(acao) : DepartamentoAnswer.DeErro("Ocorreu um erro ao tentar salvar os dados");
                }

                return DepartamentoAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return DepartamentoAnswer.DeErro(ex.Message);
            }

        }
    }
}
