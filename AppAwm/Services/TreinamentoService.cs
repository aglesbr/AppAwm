using AppAwm.DAL;
using AppAwm.Models;
using AppAwm.Respostas;
using AppAwm.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppAwm.Services
{
    public class TreinamentoService : ITreinamento<TreinamentoAnswer>
    {
        private GenericRepositoryValidation.GenericRepositoryExceptionStatus status;

        public TreinamentoAnswer Get(Expression<Func<Treinamento, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Treinamento>(db, out status);

                Treinamento? treinamento = contexto.GetItem(predicate);

                return treinamento != null ? TreinamentoAnswer.DeSucesso(treinamento) : TreinamentoAnswer.DeErro("Nenhum regisro encontrado");
            }
            catch (Exception ex)
            {
                return TreinamentoAnswer.DeErro($"Ocorreu um erro na execução da consulta - ERRO:  {ex.Message}");
            }
        }

        public TreinamentoAnswer List(Expression<Func<Treinamento, bool>> predicate)
        {
            try
            {
                using DbCon db = new();
                using var contexto = new RepositoryGeneric<Treinamento>(db, out status);

                List<Treinamento> list = [..contexto.GetAll(predicate).Include(h => h.Habilidades)];

                return list.Count > 0 ? TreinamentoAnswer.DeSucesso(list) : TreinamentoAnswer.DeErro("Nenhum regisro encontrado");
            }
            catch (Exception ex)
            {
                return TreinamentoAnswer.DeErro($"Ocorreu um erro na execução da consulta - ERRO:  {ex.Message}");
            }
        }
    }
}
