﻿using AppAwm.Models;
using AppAwm.Models.Enum;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IUsuario<T> where T : class
    {
        T Save(Usuario usuario, EnumAcao acao);
        T List(Expression<Func<Usuario, bool>> predicate);
        T Get(Expression<Func<Usuario, bool>> predicate, EnumAcao acao);
        List<Empresa> GetEmpresas(Expression<Func<Empresa, bool>> predicate);

        void OpenTransaction();
        void CloseTransaction();
    }
}
