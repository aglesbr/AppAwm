using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class LogExceptionService : ILogException<LogExceptionAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public LogExceptionAnswer Delete(LogException logException)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<LogException>(db, out status);

                int resposta = contexto.Delete(logException);

                return resposta > 0 ? LogExceptionAnswer.DeSucesso() : LogExceptionAnswer.DeErro("Ocorreu algum erro ao tentar remover o item.");
            }
            catch
            {
                return LogExceptionAnswer.DeErro("Ocorreu um erro na execução");
            }
        }

        public LogExceptionAnswer List(Expression<Func<LogException, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<LogException>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<LogException> list = [.. contexto.GetAll(predicate).Take(100)];

                    return LogExceptionAnswer.DeSucesso(list);
                }

                return LogExceptionAnswer.DeErro("Não foi possivel estabelecer conexão com o banco de dados");
            }
            catch (Exception ex)
            {
                return LogExceptionAnswer.DeErro(ex.Message);
            }
        }

        public LogExceptionAnswer Save(LogException logException)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<LogException>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    int ret = contexto.Create(logException);

                    return ret > 0 ? LogExceptionAnswer.DeSucesso() : LogExceptionAnswer.DeErro("Ocorreu um erro ao tentar registar a log de erro"); ;
                }

                return LogExceptionAnswer.DeErro("erro de conexão com o banco de dados");
            }
            catch (Exception ex)
            {
                return LogExceptionAnswer.DeErro("erro de conexão com o banco de dados");
            }
        }
    }
}
