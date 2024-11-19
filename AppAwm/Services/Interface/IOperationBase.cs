using AppAwm.Models.Enum;
using AppAwm.Models;
using System.Linq.Expressions;
using AppAwm.Respostas;

namespace AppAwm.Services.Interface
{

    public interface IOperationBase<T, U> where T : class where U : class
    {
        T Save(U objetoSave, EnumAcao acao);
        T List(Expression<Func<U, bool>> predicate);
        T Get(Expression<Func<U, bool>> predicate);
        bool Check(Expression<Func<U, bool>> predicate);
    }

}
