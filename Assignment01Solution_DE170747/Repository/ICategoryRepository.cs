using BusinessLogic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category?> GetCategory(int categoryID);
        Task<int> AddCategory(Category category);
        Task<int> UpdateCategory(int id, Category category);
    }
    
    public class CategoryRepository : ICategoryRepository
    {
        public async Task<int> AddCategory(Category category)
        {
            return await CategoyDAO.Instance().AddCategory(category);
        }

        public async Task<Category?> GetCategory(int categoryID)
        {
            return await CategoyDAO.Instance().GetCategory(categoryID);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await CategoyDAO.Instance().GetCategories();
        }

        public async Task<int> UpdateCategory(int id, Category category)
        {
            return await CategoyDAO.Instance().UpdateCategory(id,category);
        }
    }
}
