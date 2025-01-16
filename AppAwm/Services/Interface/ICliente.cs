using AppAwm.Models;
using AppAwm.Models.Enum;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface ICliente<T> where T : class
    {
        T Save(Cliente cliente, EnumAcao acao);
        T List(Expression<Func<Cliente, bool>> predicate);
        T Get(Expression<Func<Cliente, bool>> predicate);
    }
}
