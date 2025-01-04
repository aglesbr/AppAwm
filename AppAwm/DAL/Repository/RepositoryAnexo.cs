using AppAwm.DAL.Interface;
using AppAwm.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppAwm.DAL.Repository
{
    public class RepositoryAnexo : IOperationAnexo, IDisposable
    {
        private readonly DbCon dbContext;
        private bool disposedValue;

        public RepositoryAnexo(DbCon dataContext, out GenericRepositoryValidation.GenericRepositoryExceptionStatus status)
        {
            status = GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success;
            if (dataContext == null) status = GenericRepositoryValidation.GenericRepositoryExceptionStatus.ArgumentNullException;

            dbContext = dataContext!;
        }

       

        public IQueryable<Anexo> GetAll(Expression<Func<Anexo, bool>> predicate, bool hasDocumnt = false)
        {
            try
            {
                return dbContext.Anexos
                    .Where(predicate!)
                    .Select(a => new Anexo
                    {
                        Cd_Anexo = a.Cd_Anexo,
                        Cd_Empresa_Id = a.Cd_Empresa_Id,
                        Cd_Funcionario_Id = a.Cd_Funcionario_Id,
                        Cd_UsuarioAnalista = a.Cd_UsuarioCriacao,
                        Cd_UsuarioCriacao = a.Cd_UsuarioCriacao,
                        Descricao = a.Descricao,
                        TipoAnexo = a.TipoAnexo,
                        Id_UsuarioCriacao = a.Id_UsuarioCriacao,
                        Dt_Criacao = a.Dt_Criacao,
                        MotivoRejeicao = a.MotivoRejeicao,
                        MotivoResalva = a.MotivoResalva,
                        Nome = a.Nome,
                        Status = a.Status,
                        Dt_Validade_Documento = a.Dt_Validade_Documento,
                        Arquivo = hasDocumnt ? a.Arquivo :  null,
                    }).AsNoTracking();
            }
            catch (Exception) { throw; };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dbContext?.Dispose();
                }

                disposedValue = true;
            }
        }

        ~RepositoryAnexo() { Dispose(disposing: false); }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
