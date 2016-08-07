using Seven7c.OA.DALFactory;
using Seven7c.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.BLL
{
    public abstract class BaseService<T> where T:class,new()
    {
        protected IDbSession dbSession;
        public BaseService()
        {
            dbSession = DbSessionFactory.GetCurrentDbSession();
            SetCurrentDal();
        }

        public IBaseDal<T> CurrentDal;
        public abstract void SetCurrentDal();



        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            dbSession.SaveChanges();
            return entity;
        }


        public bool Update(T enetity)
        {
            CurrentDal.Update(enetity);
            return dbSession.SaveChanges() > 0;
        }
        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return dbSession.SaveChanges() > 0;
        }

        public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.LoadEntities(whereLambda);
        }

        public IQueryable<T> LoadPageEntities<S>(int pageSize, int pageIndex,
                                                 out int totalCount,
                                                 Expression<Func<T, bool>> whereLambda, bool isAsc,
                                                 Expression<Func<T, S>> orderBy)
        {
            return CurrentDal.LoadPageEntities(pageSize, pageIndex, out totalCount, whereLambda, isAsc, orderBy);
        }
    }
}
