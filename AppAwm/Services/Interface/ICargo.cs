using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface ICargo<T> where T : class
    {
        T Save(Cargo cargo);
        T List(Expression<Func<Cargo, bool>> predicate);
        T UpdateStatus(int id, string id_documento, bool isAtivo);
        List<DocumentacaoCargo> GetDocumentoVsCargo(Expression<Func<DocumentacaoCargo, bool>> predicate);
    }
}
