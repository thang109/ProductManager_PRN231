using BookShopBusiness;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookShopDataAccess
{
    public class ShippingsDAO
    {
        public static List<Shippings> GetShippings()
        {
            using (var context = new MyDbContext())
            {
                return context.Shippings.Include(o => o.UserOrder).Include(a => a.UserApprove).Include(b => b.Books).ToList();
            }
        }

        public static Shippings GetShippingById(int id)
        {
            using (var context = new MyDbContext())
            {
                return context.Shippings.Include(o => o.UserOrder).Include(a => a.UserApprove).Include(b => b.Books).FirstOrDefault(s => s.ShippingId == id);
            }
        }

        public static void InsertShipping(Shippings shipping)
        {
            using (var context = new MyDbContext())
            {
                context.Shippings.Add(shipping);
                context.SaveChanges();
            }
        }

        public static void UpdateShipping(Shippings shipping)
        {
            using (var context = new MyDbContext())
            {
                context.Shippings.Update(shipping);
                context.SaveChanges();
            }
        }

        public static void DeleteShipping(Shippings shipping)
        {
            using (var context = new MyDbContext())
            {
                context.Shippings.Remove(shipping);
                context.SaveChanges();
            }
        }
    }
}
