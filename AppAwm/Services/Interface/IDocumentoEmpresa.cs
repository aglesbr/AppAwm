using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IDocumentoEmpresa<T> where T : class
    {
        T Save(DocumentacaoEmpresa documentacaoEmpresa, bool vincular);
        T Save(DocumentacaoEmpresa documentacaoEmpresa);
        T Get(Expression<Func<DocumentacaoEmpresa, bool>>? predicate);
        T List(Expression<Func<DocumentacaoEmpresa, bool>> predicate);
    }
}
