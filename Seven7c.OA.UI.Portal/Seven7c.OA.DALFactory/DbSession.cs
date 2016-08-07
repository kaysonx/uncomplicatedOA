using Seven7c.OA.EFDAL;
using Seven7c.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.DALFactory
{
    public partial class DbSession : IDbSession
    {

        //private IUserInfoDal _UserInfoDal;
        //private IOrderInfoDal _OrderInfoDal;

        //public IUserInfoDal UserInfoDal
        //{
        //    get { 
        //        if(_UserInfoDal == null)
        //        {
        //            _UserInfoDal = DalFactory.GetUserInfoDal();
        //        }
        //        return _UserInfoDal;
        //    }
        //}

        //public IOrderInfoDal OrderInfoDal
        //{
        //    get
        //    {
        //        if (_OrderInfoDal == null)
        //        {
        //            _OrderInfoDal = DalFactory.GetOrderInfoDal();
        //        }
        //        return _OrderInfoDal;
        //    }
        //}

        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }
    }
}
