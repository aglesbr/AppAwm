using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IDownload<T> where T : class
    {
        T Save(Download download);
        T Get(int id);
        T GetAll(Expression<Func<Download, bool>> prdicate);
        T Remover(Download download);
    }
}
