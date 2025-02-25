using AppAwm.Models;
using AppAwm.Models.Enum;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IEmpresa<T> where T : class
    {
        T Save(Empresa empresa, EnumAcao acao);
        T List(Expression<Func<Empresa, bool>> predicate);
        T ListChart(Expression<Func<Empresa, bool>> predicate);
        T Get(Expression<Func<Empresa, bool>> predicate);
        ValueTask<T> GetCnpj(string cnpj);
        int Vincular(ColaboradorVinculoObra vinculoObra);
        List<Cliente> GetClientes(Expression<Func<Cliente, bool>> predicate);
        T Remove(int id);
    }
}
