using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class DocumentoEmpresaService : IDocumentoEmpresa<DocumentoEmpresaAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public DocumentoEmpresaAnswer Get(Expression<Func<DocumentacaoEmpresa, bool>>? predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoEmpresa>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    DocumentacaoEmpresa? documentacaoEmpresa = contexto.GetItem(predicate);

                    return documentacaoEmpresa != null ? DocumentoEmpresaAnswer.DeSucesso(documentacaoEmpresa) : DocumentoEmpresaAnswer.DeErro("Nenhum item foi localizado");
                }

                return DocumentoEmpresaAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DocumentoEmpresaAnswer.DeErro(ex.Message);
            }
        }

        public DocumentoEmpresaAnswer List(Expression<Func<DocumentacaoEmpresa, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoEmpresa>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<DocumentacaoEmpresa> lst = [.. contexto.GetAll(predicate).Take(100)];

                    return lst.Count > 0 ? DocumentoEmpresaAnswer.DeSucesso(lst) : DocumentoEmpresaAnswer.DeErro("Nenhum item foi localizado");
                }

                return DocumentoEmpresaAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DocumentoEmpresaAnswer.DeErro(ex.Message);
            }
        }

        public DocumentoEmpresaAnswer Save(DocumentacaoEmpresa documentacaoEmpresa, bool vincular)
        {
            try
            {
                int ret = 0;
                DocumentoEmpresaAnswer? resposta = null;

                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoEmpresa>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {

                    DocumentacaoEmpresa? checkDocumentoEmpresa = contexto.GetItem(g =>
                        g.Cd_Documento_Id == documentacaoEmpresa.Cd_Documento_Id
                        && g.Cd_Empresa_Id == (documentacaoEmpresa.Cd_Empresa_Id > 0 ? documentacaoEmpresa.Cd_Empresa_Id : g.Cd_Empresa_Id)
                    );

                    if (vincular)
                    {
                        List<string> items = [.. checkDocumentoEmpresa!.Cd_Documentos_Complementares_Id!.Split(',')];
                        items.Add(documentacaoEmpresa.Cd_Documentos_Complementares_Id!);
                        items.Sort();
                        checkDocumentoEmpresa.Cd_Documentos_Complementares_Id = string.Join(',', items);

                        ret = contexto.Edit(checkDocumentoEmpresa!);

                        resposta = ret > 0 ? DocumentoEmpresaAnswer.DeSucesso(documentacaoEmpresa) : DocumentoEmpresaAnswer.DeErro("Ocorreu um erro ao tentar vincular função com o tipo dedocumento");
                    }
                    else
                    {

                        List<string> items = [.. checkDocumentoEmpresa!.Cd_Documentos_Complementares_Id!.Split(',')];
                        items.Remove(documentacaoEmpresa.Cd_Documentos_Complementares_Id!);
                        checkDocumentoEmpresa.Cd_Documentos_Complementares_Id = string.Join(',', items);

                        ret = contexto.Edit(checkDocumentoEmpresa!);

                        resposta = ret > 0 ? DocumentoEmpresaAnswer.DeSucesso(documentacaoEmpresa!) : DocumentoEmpresaAnswer.DeErro("Ocorreu um erro ao tentar desvincular função com o tipo dedocumento");
                    }

                    return resposta;
                }

                return DocumentoEmpresaAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DocumentoEmpresaAnswer.DeErro(ex.Message);
            }
        }

        public DocumentoEmpresaAnswer Save(DocumentacaoEmpresa documentacaoEmpresa)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoEmpresa>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    int ret = contexto.Create(documentacaoEmpresa);
                    DocumentoEmpresaAnswer resposta = ret > 0 ? DocumentoEmpresaAnswer.DeSucesso("Inserido com suacesso") : DocumentoEmpresaAnswer.DeErro("não foi possivel inserir os codigos dos documentos");
                    return resposta;
                }

                return DocumentoEmpresaAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DocumentoEmpresaAnswer.DeErro(ex.Message);
            }
        }
    }
}
