using BookShopBusiness;
using BookShopDataAccess;
using System.Collections.Generic;

namespace BookShopRepository
{
    public class UsersRepository : IUsersRepository
    {
        public void DeleteUser(Users user) => UsersDAO.DeleteUser(user);
        public void SaveUser(Users user) => UsersDAO.InsertUser(user);
        public void UpdateUser(Users user) => UsersDAO.UpdateUser(user);
        public List<Users> GetAllUsers() => UsersDAO.GetUsers();
        public Users GetUserById(int id) => UsersDAO.GetUserById(id);
    }
}
