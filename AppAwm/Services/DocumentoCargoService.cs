using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class DocumentoCargoService : IDocumentoCargo<DocumentoCargoAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public DocumentoCargoAnswer Get(Expression<Func<DocumentacaoCargo, bool>>? predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoCargo>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    DocumentacaoCargo? documentacaoCargo = contexto.GetItem(predicate);

                    return documentacaoCargo != null ? DocumentoCargoAnswer.DeSucesso(documentacaoCargo) : DocumentoCargoAnswer.DeErro("Nenhum item foi localizado");
                }

                return DocumentoCargoAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DocumentoCargoAnswer.DeErro(ex.Message);
            }
        }

        public DocumentoCargoAnswer List(Expression<Func<DocumentacaoCargo, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoCargo>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<DocumentacaoCargo> lst = [.. contexto.GetAll(predicate).Take(100)];

                    return lst.Count > 0 ? DocumentoCargoAnswer.DeSucesso(lst) : DocumentoCargoAnswer.DeErro("Nenhum item foi localizado");
                }

                return DocumentoCargoAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DocumentoCargoAnswer.DeErro(ex.Message);
            }
        }

        public DocumentoCargoAnswer Save(DocumentacaoCargo documentacaoCargo, bool vincular)
        {
            try
            {
                int ret = 0;
                DocumentoCargoAnswer? resposta = null;

                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoCargo>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {

                    DocumentacaoCargo? checkDocumentoCargo = contexto.GetItem(g =>
                        g.Cd_Documento_Id == documentacaoCargo.Cd_Documento_Id
                        && g.Cd_Cargo_Id == documentacaoCargo.Cd_Cargo_Id
                        && g.Cd_Empresa_Id == (documentacaoCargo.Cd_Empresa_Id > 0 ? documentacaoCargo.Cd_Empresa_Id : g.Cd_Empresa_Id)
                    );

                    if (vincular)
                    {
                        if (checkDocumentoCargo is null)
                        {
                            ret = contexto.Create(documentacaoCargo);
                        }
                        else if (documentacaoCargo.Cd_Empresa_Id == null && checkDocumentoCargo!.Cd_Empresa_Id != null)
                        {
                            checkDocumentoCargo.Cd_Empresa_Id = null;
                            ret = contexto.Edit(checkDocumentoCargo);
                        }

                        resposta = ret > 0 ? DocumentoCargoAnswer.DeSucesso(documentacaoCargo) : DocumentoCargoAnswer.DeErro("Ocorreu um erro ao tentar vincular função com o tipo dedocumento");
                    }
                    else
                    {

                        ret = contexto.Delete(checkDocumentoCargo!);
                        resposta = ret > 0 ? DocumentoCargoAnswer.DeSucesso(checkDocumentoCargo!) : DocumentoCargoAnswer.DeErro("Ocorreu um erro ao tentar desvincular função com o tipo dedocumento");
                    }

                    return resposta;
                }

                return DocumentoCargoAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DocumentoCargoAnswer.DeErro(ex.Message);
            }
        }
    }
}
