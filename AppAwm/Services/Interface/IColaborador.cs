using AppAwm.Models;
using AppAwm.Models.Enum;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IColaborador<T> where T : class
    {
        T Save(Colaborador usuario, EnumAcao acao);
        T List(Expression<Func<Colaborador, bool>> predicate);
        T Get(Expression<Func<Colaborador, bool>> predicate, Usuario? usuario);
        bool Check(Expression<Func<Colaborador, bool>> predicate);
        int UpdateFoto(Colaborador funcionario);
        List<Cargo> GetCargos(string nome);
        List<Empresa> GetEmpresas(Expression<Func<Empresa, bool>> predicate);
        Cracha? GetCracha(int? id = 0);
        int UpdateCliente(bool isAdd, int? count = null);
        bool CheckClienteVidasDisponivel();
        T ImportarColaboradore(MemoryStream stream, Usuario? usuario, int cd_empresa);
    }

}
