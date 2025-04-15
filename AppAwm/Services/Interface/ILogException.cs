using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface ILogException<T> where T : class
    {
        T Save(LogException logException);
        T Delete(LogException logException);
        T List(Expression<Func<LogException, bool>> predicate);

    }
}
