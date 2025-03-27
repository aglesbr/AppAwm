using AppAwm.Models;
using AppAwm.Models.Enum;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IVideo<T> where T : class
    {
        T Save(Video video, EnumAcao acao);
        T List(Expression<Func<Video, bool>> predicate);
        T Get(Expression<Func<Video, bool>> predicate);
        T Remover(Video video);
    }
}
