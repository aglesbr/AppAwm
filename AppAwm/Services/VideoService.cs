using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class VideoService : IVideo<VideoAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public VideoAnswer Remover(Video video)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Video>(db, out status);

                int resposta = contexto.Delete(video);

                return resposta > 0 ? VideoAnswer.DeSucesso("Video removido com sucesso.") : VideoAnswer.DeErro("Ocorreu algum erro ao tentar remover o item.");
            }
            catch
            {
                return VideoAnswer.DeErro("Ocorreu um erro na execução");
            }
        }

        public VideoAnswer Get(Expression<Func<Video, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Video>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    Video? video = contexto.GetAll(predicate).FirstOrDefault();

                    return video is not null ? VideoAnswer.DeSucesso(video, false) : VideoAnswer.DeFalha("Nenhum registro fui localizado");
                }

                return VideoAnswer.DeFalha("Não foi possivel estabelecer conexão com o banco de dados");
            }
            catch (Exception ex)
            {
                return VideoAnswer.DeFalha(ex.Message);
            }
        }

        public VideoAnswer List(Expression<Func<Video, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Video>(db, out status);

                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Video> video = [.. contexto.GetAll(predicate).OrderBy(o => o.Titulo)];
                    return video.Count > 0 ? VideoAnswer.DeSucesso(video) : VideoAnswer.DeFalha("Nenhum registro fui localizado");
                }

                return VideoAnswer.DeFalha("Não foi possivel estabelecer conexão com o banco de dados");
            }
            catch (Exception ex)
            {
                return VideoAnswer.DeFalha(ex.Message);
            }
        }

        public VideoAnswer Save(Video video, EnumAcao acao)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Video>(db, out status);

                int ret = acao == EnumAcao.Criar ? contexto.Create(video) : contexto.Edit(video);

                return ret > 0 ? VideoAnswer.DeSucesso(video, acao == EnumAcao.Criar) : VideoAnswer.DeErro("Ocorreu um erro ao tentar salvar");
            }
            catch (Exception ex)
            {
                return VideoAnswer.DeErro(ex.Message);
            }
        }
    }
}
