using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class DocumentacaoComplementarService : IDocumentacaoComplementar<DocumentacaoComplementarAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public DocumentacaoComplementarAnswer Get(Expression<Func<DocumentacaoComplementar, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoComplementar>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<DocumentacaoComplementar> lst = [.. contexto.GetAll(predicate)];

                    return lst.Count > 0 ? DocumentacaoComplementarAnswer.DeSucesso(lst) : DocumentacaoComplementarAnswer.DeErro("Nenhum item foi localizado");
                }

                return DocumentacaoComplementarAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DocumentacaoComplementarAnswer.DeErro(ex.Message);
            }
        }
    }
}
