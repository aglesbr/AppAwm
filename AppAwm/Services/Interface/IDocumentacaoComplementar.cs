﻿using AppAwm.Models;
using System.Linq.Expressions;

namespace AppAwm.Services.Interface
{
    public interface IDocumentacaoComplementar<T> where T : class
    {
        T Get(Expression<Func<DocumentacaoComplementar, bool>> predicate);
    }
}
