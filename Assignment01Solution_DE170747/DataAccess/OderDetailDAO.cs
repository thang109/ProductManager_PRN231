using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OderDetailDAO
    {
        private static OderDetailDAO? instance;
        private static  readonly object lockObj = new object();
        private OderDetailDAO() { }
        public OderDetailDAO Instance()
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new OderDetailDAO();
                        }
                    }
                }
                return instance;
        }
        public async Task<int> CreateOrdetail(int userId,OrderDetail orderDetail)
        {
            using(var context = new EStoreContext())
            {
                await context.OrderDetails.AddAsync(orderDetail);
                await context.SaveChangesAsync();
                return orderDetail.OrderId;
            }
        }
    }
}
