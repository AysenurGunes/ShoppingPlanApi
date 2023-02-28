﻿using System.Linq.Expressions;

namespace ShoppingPlanApi.DataAccess
{
    public interface IShoppingPlan<T>
    {
        IList<T> GetAll();
        T GetByID(Expression<Func<T, bool>> expression);
        IList<T> GetSpecial(Expression<Func<T, bool>> expression);

        int Add(T entity);
        int Edit(T entity);
        int Delete(T entity);
    }
}
