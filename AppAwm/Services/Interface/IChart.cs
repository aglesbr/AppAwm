using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IChart<T> where T : class
    {
        T Get(Expression<Func<Anexo, bool>>? predicate, Usuario usuario, int origem);
    }
}
