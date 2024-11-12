using BookShopBusiness;
using BookShopDataAccess;
using System.Collections.Generic;

namespace BookShopRepository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public void DeleteCategory(Categories category) => CategoriesDAO.DeleteCategory(category);
        public void SaveCategory(Categories category) => CategoriesDAO.InsertCategory(category);
        public void UpdateCategory(Categories category) => CategoriesDAO.UpdateCategory(category);
        public List<Categories> GetAllCategories() => CategoriesDAO.GetCategories();
        public Categories GetCategoryById(int id) => CategoriesDAO.GetCategoryById(id);
    }
}
