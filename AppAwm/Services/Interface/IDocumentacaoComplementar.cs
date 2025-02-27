using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IDocumentacaoComplementar<T> where T : class
    {
        T Get(Expression<Func<DocumentacaoComplementar, bool>> predicate);
        T GetTipoDocumento(int cd_codigo_id,int cd_emprsa_id,int origem = 1);
    }
}
