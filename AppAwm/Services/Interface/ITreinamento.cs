using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface ITreinamento<T> where T : class
    {
        T Get(Expression<Func<Treinamento, bool>> predicate);
        T List(Expression<Func<Treinamento,bool>> predicate);
    }
}
