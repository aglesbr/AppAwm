using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IDocumentoCargo<T> where T : class
    {
        T Save(DocumentacaoCargo documentacaoCargo, bool vincular);
        T Get(Expression<Func<DocumentacaoCargo, bool>>? predicate);
        T List(Expression<Func<DocumentacaoCargo, bool>> predicate);
    }
}
