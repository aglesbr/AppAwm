using AppAwm.Models.Enum;
using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IAnexo<T> where T : class
    {
        T Save(Anexo anexo);
        T List(Expression<Func<Anexo, bool>> predicate);
        T Remove(Anexo anexo);
        T UpdateStatus(int id, EnumStatusDocs statusDocs, string usuario, string? message =  null);
        HistoricoExecucao? GetLastHistoricoExecucao();
        int InsereProximaExecucao(HistoricoExecucao historicoExecucao);
        void Notify();
    }
}
