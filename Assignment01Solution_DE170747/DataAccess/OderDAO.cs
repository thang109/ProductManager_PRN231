using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OderDAO
    {
        private static OderDAO? instance;
        private static readonly object lockObj = new object();
        private OderDAO() { }
        public static OderDAO Instance()
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new OderDAO();
                    }
                }
            }
            return instance;
        }

        public async Task<int> CreateOrder(int productId, int quantity, decimal discount, Order order)
        {
            using (var context = new EStoreContext())
            {
                // Kiểm tra sản phẩm có tồn tại hay không trước khi thêm Order và OrderDetail
                var product = await context.Products.FindAsync(productId);
                if (product == null || product.UnitInStock < quantity)
                {

                    return -1;
                }

                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();  

                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = productId,
                    Quantity = quantity,
                    Discount = discount
                };
                await context.OrderDetails.AddAsync(orderDetail);
                await context.SaveChangesAsync();  
                product.UnitInStock -= quantity;

                context.Products.Update(product);

                await context.SaveChangesAsync();

                return order.OrderId;
            }
        }



        public async Task<Order?> GetOrder(int orderID)
        {
            using(var context = new EStoreContext())
            {
                return await context.Orders.FindAsync(orderID);
            }
        }

        public async Task<List<Order>> GetOrderByMemberId(int memberId)
        {
            using(var context = new EStoreContext())
            {
               return await context.Orders.Where(o => o.MemberId == memberId).AsNoTracking().ToListAsync();
            }
        }
    }
}
