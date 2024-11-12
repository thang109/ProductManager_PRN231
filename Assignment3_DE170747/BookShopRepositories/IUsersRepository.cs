using BookShopBusiness;
using System.Collections.Generic;

namespace BookShopRepository
{
    public interface IUsersRepository
    {
        List<Users> GetAllUsers();
        Users GetUserById(int id);
        void SaveUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(Users user);
    }
}
