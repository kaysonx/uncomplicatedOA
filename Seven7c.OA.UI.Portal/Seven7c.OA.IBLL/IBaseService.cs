using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.IBLL
{
    public interface IBaseService<T> where T : class,new()
    {
        T Add(T entity);
        bool Update(T enetity);
        bool Delete(T entity);

        IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> LoadPageEntities<S>(int pageSize, int pageIndex,
                                          out int totalCount,
                                          Expression<Func<T, bool>> whereLambda, bool isAsc, Expression<Func<T, S>> orderBy);
    }
}
