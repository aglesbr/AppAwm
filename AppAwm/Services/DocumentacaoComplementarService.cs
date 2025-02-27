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

        public DocumentacaoComplementarAnswer GetTipoDocumento(int cd_codigo_id, int cd_empresa_id, int origem = 1)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoComplementar>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<DocumentacaoCargo>? documentacaoCargo =  null;
                    List<DocumentacaoEmpresa>? documentacaoEmpresa = null;


                    if (origem == 1)
                    {
                        documentacaoCargo = [.. db.DocumentacaoCargos.Where(p => p.Cd_Cargo_Id == cd_codigo_id && p.Cd_Empresa_Id == (cd_empresa_id > 0 ? cd_empresa_id : p.Cd_Empresa_Id) )];
                    }
                    else
                    {
                        documentacaoEmpresa = [.. db.DocumentacaoEmpresas.Where(p => p.Cd_Empresa_Id == cd_codigo_id)];
                    }
                    //var filter = db.DocumentacaoCargos.Where(s => s.Cd_Cargo_Id == cd_codigo_id).ToList();
                    //filter.RemoveAll(r => r.Cd_Empresa_Id != null && r.Cd_Empresa_Id != cd_empresa);

                    //List<int> list = filter.Select(ss => ss.Cd_Documento_Id).ToList();


                    DocumentacaoComplementarAnswer resposa = Get(s => s.Origem == origem);

                    resposa.DocumentacaoComplementares.ForEach(f =>
                    {
                        if (origem == 1)
                        {
                            f.Vinculado = documentacaoCargo!.Any(a => a.Cd_Documento_Id.ToString() == f.Cd_DocumentoComplementar_Id);
                        }
                        else
                        {
                            List<int> items = [..documentacaoEmpresa!.FirstOrDefault()!.Cd_Documentos_Complementares_Id!.Split(',').Select(Int32.Parse)];
                            f.Vinculado = items.Any(a => a == f.Cd_Documentaco_Complementar);
                        }
                    });

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
