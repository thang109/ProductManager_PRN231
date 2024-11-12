using BusinessLogic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(int productId, int quantity, decimal discount, Order order);
        Task<Order?> GetOrder(int orderID);
        Task<List<Order>> GetOrderByMemberId(int memberId);
    }

    public class OrderRepository : IOrderRepository
    {
        public async Task<int> CreateOrder(int productId, int quantity, decimal discount, Order order)
        {
            return await OderDAO.Instance().CreateOrder(productId, quantity, discount, order);
        }

        public Task<Order?> GetOrder(int orderID)
        {
            return OderDAO.Instance().GetOrder(orderID);
        }

        public Task<List<Order>> GetOrderByMemberId(int memberId)
        {
            return OderDAO.Instance().GetOrderByMemberId(memberId);
        }
    }
}
