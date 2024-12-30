using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class ObraService : IObra<ObraAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public ObraAnswer List(Expression<Func<Obra, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Obra>(db, out status);

                List<Obra> list = contexto.GetAll(predicate).ToList();

                return list.Count > 0 ? ObraAnswer.DeSucesso(list) : ObraAnswer.DeErro("Nenhum regisro encontrado");
            }
            catch (Exception ex)
            {
                return ObraAnswer.DeErro($"Ocorreu um erro na execução da consulta - ERRO:  {ex.Message}");
            }
        }

        public ObraAnswer Save(Obra obra)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Obra>(db, out status);

                string msg = obra.Cd_Obra > 0 ? "atualizada" : "inserida";

                if (obra.Cd_Obra > 0)
                {
                    obra.Cd_Usuario_Atualizacao = obra.Cd_Usuario_Criacao;
                    obra.Dt_Atualizacao = DateTime.Now.Date;
                }

                int ret = obra.Cd_Obra == 0 ? contexto.Create(obra) : contexto.Edit(obra);

                return ret > 0 ? ObraAnswer.DeSucesso(msg) : ObraAnswer.DeErro("Erro ao tentar salvar uma obra");
            }
            catch (Exception ex)
            {
                return ObraAnswer.DeErro($"Ocorreu um erro ao tentar registrar uma opbra: ERRO {ex.Message}");
            }
        }

        public ObraAnswer SeVinculado(Expression<Func<ColaboradorVinculoObra, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<ColaboradorVinculoObra>(db, out status);

                var retorno = contexto.GetAll(predicate).Any();

                return retorno ? ObraAnswer.DeErro("Existem colaboradores vinculados a essa obra") : ObraAnswer.DeSucesso("Nenhum colaborador vinculado a essa obra.");
            }
            catch (Exception ex)
            {
                return ObraAnswer.DeErro($"Erro ao tentar chechar o vinculo de obra: {ex.Message}");
            }
        }
    }
}
