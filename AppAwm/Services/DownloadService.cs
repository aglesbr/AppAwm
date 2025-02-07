using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class DownloadService : IDownload<DownloadAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public DownloadAnswer Get(int id)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Download>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    Download? download= contexto.GetItem(g => g.Cd_Download.Equals(id));

                    return download is not null ? DownloadAnswer.DeSucesso(download) : DownloadAnswer.DeErroOuVazio("Nenhum registro fui localizado");
                }

                return DownloadAnswer.DeErroOuVazio("Não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DownloadAnswer.DeErroOuVazio(ex.Message);
            }
        }

        public DownloadAnswer GetAll(Expression<Func<Download, bool>> prdicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Download>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Download>? downloads = [..contexto.GetAll(prdicate)];

                    return downloads is not null ? DownloadAnswer.DeSucesso(downloads) : DownloadAnswer.DeErroOuVazio("Nenhum registro fui localizado");
                }

                return DownloadAnswer.DeErroOuVazio("Não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DownloadAnswer.DeErroOuVazio(ex.Message);
            }
        }

        public DownloadAnswer Remover(Download download)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Download>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    int ret = contexto.Delete(download);
                    return ret > 0 ? DownloadAnswer.DeSucesso("Arquivo excluído com sucesso.") : DownloadAnswer.DeErroOuVazio("Ocorreu um erro ao tentar excluir o anexo"); ;
                }

                return DownloadAnswer.DeErroOuVazio("Não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DownloadAnswer.DeErroOuVazio(ex.Message);
            }
        }

        public DownloadAnswer Save(Download download)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Download>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    int ret = contexto.Create(download);
                    return ret > 0 ? DownloadAnswer.DeSucesso() : DownloadAnswer.DeErroOuVazio("Ocorreu um erro ao tentar registar o anexo"); ;
                }

                return DownloadAnswer.DeErroOuVazio("Não foi possivel conectar com o banco de dados");
            }
            catch (Exception ex)
            {
                return DownloadAnswer.DeErroOuVazio(ex.Message);
            }
        }
    }
}
