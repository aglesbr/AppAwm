using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IObra<T> where T : class
    {
        T Save(Obra anexo);
        T List(Expression<Func<Obra, bool>> predicate);
        T SeVinculado(Expression<Func<ColaboradorVinculoObra, bool>> predicate);
    }
}
