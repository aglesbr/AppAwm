using AppAwm.Models;
using AppAwm.Models.Enum;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IFuncionario<T> where T : class
    {
        T Save(Funcionario usuario, EnumAcao acao);
        T List(Expression<Func<Funcionario, bool>> predicate);
        T Get(Expression<Func<Funcionario, bool>> predicate, Usuario? usuario);
        bool Check(Expression<Func<Funcionario, bool>> predicate);
        int UpdateFoto(Funcionario funcionario);
        List<Cargo> GetCargos(string nome);
        List<Empresa> GetEmpresas(Expression<Func<Empresa, bool>> predicate);
        Cracha? GetCracha(int? id = 0);
        int UpdateCliente(bool isAdd);
    }
}
