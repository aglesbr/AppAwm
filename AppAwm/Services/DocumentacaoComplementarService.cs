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

        public DocumentacaoComplementarAnswer GetDocumentoCargo(int cd_codigo_id, int? cd_empresa, int origem = 1)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoComplementar>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<DocumentacaoCargo> documentacaoCargo = [.. db.DocumentacaoCargos.Where(p => p.Cd_Cargo_Id == cd_codigo_id)];

                    documentacaoCargo.RemoveAll(r => r.Cd_Empresa_Id != null && r.Cd_Empresa_Id != cd_empresa);

                    //var filter = db.DocumentacaoCargos.Where(s => s.Cd_Cargo_Id == cd_codigo_id).ToList();
                    //filter.RemoveAll(r => r.Cd_Empresa_Id != null && r.Cd_Empresa_Id != cd_empresa);

                    //List<int> list = filter.Select(ss => ss.Cd_Documento_Id).ToList();


                    DocumentacaoComplementarAnswer resposa = Get(s => s.Origem == origem);

                    resposa.DocumentacaoComplementares.ForEach(f => f.Vinculado = documentacaoCargo.Any(a => a.Cd_Documento_Id.ToString() == f.Cd_DocumentoComplementar_Id));

                   return resposa;
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
