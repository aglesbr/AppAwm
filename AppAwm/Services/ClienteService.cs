using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Models.Enum;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class ClienteService : ICliente<ClienteAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public ClienteAnswer Get(Expression<Func<Cliente, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Cliente>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    Cliente? cliente = contexto.GetAll(predicate).FirstOrDefault();

                    return cliente is not null ? ClienteAnswer.DeSucesso(cliente) : ClienteAnswer.DeFalha("Nenhum registro fui localizado");
                }

                return ClienteAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return ClienteAnswer.DeFalha(ex.Message);
            }
        }

        public ClienteAnswer List(Expression<Func<Cliente, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Cliente>(db, out status);
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    List<Cliente> cliente = [.. contexto.GetAll(predicate).OrderBy(o => o.Nome)];
                    return cliente.Count > 0 ? ClienteAnswer.DeSucesso(cliente) : ClienteAnswer.DeFalha("Nenhum registro fui localizado");
                }

                return ClienteAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return ClienteAnswer.DeFalha(ex.Message);
            }
        }

        public ClienteAnswer Save(Cliente cliente, EnumAcao acao)
        {
            using DbCon db = new();
            using var contexto = new RepositoryGeneric<Cliente>(db, out status);
            try
            {
                if (status == GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success)
                {
                    int ret = acao == EnumAcao.Criar ? contexto.Create(cliente) : contexto.Edit(cliente);

                    if (ret <= 0)
                        return ClienteAnswer.DeFalha("Ocorreu um erro ao tentar registar a empresa");

                    return ret > 0 ? ClienteAnswer.DeSucesso(cliente) : ClienteAnswer.DeErro();
                }

                return ClienteAnswer.DeFalha();
            }
            catch (Exception ex)
            {
                return ClienteAnswer.DeFalha(ex.Message);
            }
        }
    }
}
