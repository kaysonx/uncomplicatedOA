using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.EFDAL
{
    //针对所有实体的公共CRUD方法实现的封装
    public class BaseDal<T> where T:class,new()
    {
        private DbContext db;
        public BaseDal()
        {
            db = DbContextFactory.GetCurrentDbContext();
        }

        public T Add(T entity)
        {
            db.Set<T>().Add(entity);
            return entity;
        }
        public bool Update(T entity)
        {
            db.Entry(entity).State = System.Data.EntityState.Modified;
            return true;
        }
        public bool Delete(T entity)
        {
            db.Entry(entity).State = System.Data.EntityState.Deleted;
            return true;
        }


        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).AsQueryable();
        }


        public IQueryable<T> LoadPageEntities<S>(int pageSize, int pageIndex,
                                           out int totalCount,
                                            Expression<Func<T, bool>> whereLambda,
                                            bool isAsc, Expression<Func<T, S>> orderBy)
        {
            IQueryable<T> temp = db.Set<T>().Where(whereLambda).AsQueryable();
            totalCount = temp.Count();

            if(isAsc)
            {
                temp = temp.OrderBy(orderBy)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
            }
            else
            {
                temp = temp.OrderByDescending(orderBy)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize).AsQueryable();
            }
            return temp;
        }
    }
}
