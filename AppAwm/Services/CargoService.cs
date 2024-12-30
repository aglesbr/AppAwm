using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class CargoService : ICargo<CargoAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public List<DocumentacaoCargo> GetDocumentoVsCargo(Expression<Func<DocumentacaoCargo, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoCargo>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<DocumentacaoCargo> lst = [.. contexto.GetAll(predicate).Take(100)];
                    return lst;
                }

                return [];
            }
            catch
            {
                return [];
            }
        }

        public CargoAnswer List(Expression<Func<Cargo, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Cargo>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Cargo> lst = [.. contexto.GetAll(predicate).Take(100)];

                    return lst.Count > 0 ? CargoAnswer.DeSucesso(lst) : CargoAnswer.DeErro("Nenhum item foi localizado");
                }

                return CargoAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return CargoAnswer.DeErro(ex.Message);
            }
        }

        public CargoAnswer Save(Cargo cargo)
        {
            throw new NotImplementedException();
        }

        public CargoAnswer UpdateStatus(int id, string id_documento, bool isAtivo)
        {
            try
            {
                int retorno = 0;
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<DocumentacaoCargo>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    var documentoCargo = db.DocumentacaoCargos.SingleOrDefault(w => w.Cd_Cargo_Id == id && w.Cd_Documento_Id == Convert.ToInt32(id_documento));

                    if (documentoCargo is null)
                    {
                        var retornoInsert = db.DocumentacaoCargos.Add(new DocumentacaoCargo { Cd_Cargo_Id = id, Cd_Documento_Id = Convert.ToInt32(id_documento), Status = isAtivo });

                        if (retornoInsert.State == EntityState.Added)
                            retorno = retornoInsert.Context.SaveChanges(true);

                        return retorno > 0 ? CargoAnswer.DeSucesso("Inserido com sucesso") : CargoAnswer.DeErro("Não foi possivel inserir o registro");
                    }

                    retorno = db.DocumentacaoCargos.Where(w => w.Cd_Cargo_Id == id
                    && w.Cd_Documento_Id == Convert.ToInt32(id_documento))
                        .ExecuteUpdate(s => s.SetProperty(sp => sp.Status, isAtivo));

                    return retorno > 0 ? CargoAnswer.DeSucesso("Atualizado com sucesso") : CargoAnswer.DeErro("Registro não consguiu atualizar");

                }

                return CargoAnswer.DeErro("não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return CargoAnswer.DeErro(ex.Message);
            }
        }
    }
}
