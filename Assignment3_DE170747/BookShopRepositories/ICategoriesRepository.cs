using BookShopBusiness;
using System.Collections.Generic;

namespace BookShopRepository
{
    public interface ICategoriesRepository
    {
        List<Categories> GetAllCategories();
        Categories GetCategoryById(int id);
        void SaveCategory(Categories category);
        void UpdateCategory(Categories category);
        void DeleteCategory(Categories category);
    }
}
