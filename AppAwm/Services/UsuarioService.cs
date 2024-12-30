using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class UsuarioService : IUsuario<UsuarioAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public UsuarioService() { }

        public UsuarioAnswer Get(Expression<Func<Usuario, bool>> predicate, EnumAcao acao)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Usuario>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    return UsuarioAnswer.DeSucesso(contexto.GetItem(predicate) ?? new(), acao);
                }
                else
                    return UsuarioAnswer.Falha();

            }
            catch (Exception ex)
            {
                return UsuarioAnswer.Falha(ex.Message, EnumAcao.Nenhum);
            }
        }


        public UsuarioAnswer List(Expression<Func<Usuario, bool>> predicatel)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Usuario>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Usuario> usuarios = [.. contexto.GetAll(predicatel)];
                    return usuarios.Count > 0 ? UsuarioAnswer.DeSucesso(usuarios) : UsuarioAnswer.Falha("Nenhum registro fui localizado", EnumAcao.Nenhum);
                }

                return UsuarioAnswer.DeErro();
            }
            catch (Exception ex)
            {
                return UsuarioAnswer.Falha(ex.Message, EnumAcao.Nenhum);
            }
        }

        public UsuarioAnswer Save(Usuario usuario, EnumAcao acao)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Usuario>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    int ret = acao == EnumAcao.Criar ? contexto.Create(usuario) : contexto.Edit(usuario);

                    return ret > 0 ? UsuarioAnswer.DeSucesso(usuario, acao) : UsuarioAnswer.DeErro();
                }

                return UsuarioAnswer.Falha();
            }
            catch (Exception ex)
            {
                return UsuarioAnswer.Falha(ex.Message, EnumAcao.Nenhum);
            }
        }

        public void OpenTransaction()
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Empresa>(db, out status);
            //  contexto.StartTransaction();
        }

        public void CloseTransaction()
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Empresa>(db, out status);
            //  contexto.FinishTransactio();
        }

        public List<Empresa> GetEmpresas(Expression<Func<Empresa, bool>> predicate)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Empresa>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    return [.. contexto.GetAll(predicate)];
                }
                return [];
            }
            catch
            {
                throw;
            }
        }
    }
}
