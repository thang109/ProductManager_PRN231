using BookShopBusiness;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookShopDataAccess
{
    public class UsersDAO
    {
        public static List<Users> GetUsers()
        {
            using (var context = new MyDbContext())
            {
                return context.Users.ToList();
            }
        }

        public static Users GetUserById(int id)
        {
            using (var context = new MyDbContext())
            {
                return context.Users.FirstOrDefault(u => u.UserId == id);
            }
        }

        public static void InsertUser(Users user)
        {
            using (var context = new MyDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void UpdateUser(Users user)
        {
            using (var context = new MyDbContext())
            {
                context.Users.Update(user);
                context.SaveChanges();
            }
        }

        public static void DeleteUser(Users user)
        {
            using (var context = new MyDbContext())
            {
                var userToDelete = context.Users.FirstOrDefault(b => b.UserId == user.UserId);

                if (userToDelete != null)
                {
                    var listShipping = context.Shippings.Where(s => s.UserOrderId == userToDelete.UserId).ToList();
                    foreach (var shipping in listShipping)
                    {
                        context.Shippings.Remove(shipping);
                    }

                    var approvedShippings = context.Shippings.Where(s => s.UserApproveId == userToDelete.UserId).ToList();
                    foreach (var shipping in approvedShippings)
                    {
                        context.Shippings.Remove(shipping);
                    }

                    context.Users.Remove(userToDelete);

                    context.SaveChanges();
                }
            }
        }

    }
}
