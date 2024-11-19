using AppAwm.Models;
using AppAwm.Models.Enum;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IEmpresa<T> where T : class
    {
        T Save(Empresa empresa, EnumAcao acao);
        T List(Expression<Func<Empresa, bool>> predicate);
        T Get(Expression<Func<Empresa, bool>> predicate, EnumAcao acao);
        ValueTask<T> GetCnpj(string cnpj);
        int Vincular(FuncionarioVinculoObra vinculoObra);
    }
}
