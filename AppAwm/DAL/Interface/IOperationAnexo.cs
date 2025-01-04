using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.DAL.Interface
{
    public interface IOperationAnexo
    {
        IQueryable<Anexo>? GetAll(Expression<Func<Anexo, bool>> predicate, bool hasDocumnt = false);
    }
}
