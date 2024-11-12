using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoyDAO
    {
        private static CategoyDAO? instance;
        private static readonly object lockObject = new object();
        private CategoyDAO() { }
        public static CategoyDAO Instance()
        {
           if (instance is null)
            {
                lock (lockObject)
                {
                    if (instance is null)
                    {
                        instance = new CategoyDAO();
                    }
                }
            }
            return instance;
        }

        public async Task<List<Category>> GetCategories()
        {
            using (var dbcontext = new EStoreContext())
            {
                return await dbcontext.Categories.ToListAsync();
            }
        }

        public async Task<Category?> GetCategory(int categoryID)
        {
            using (var dbcontext = new EStoreContext())
            {
                return await dbcontext.Categories.FindAsync(categoryID);
            }
        }

        public async Task<int> AddCategory(Category category)
        {
            using (var dbcontext = new EStoreContext())
            {
                await dbcontext.Categories.AddAsync(category);
                await dbcontext.SaveChangesAsync();
                return category.CategoryId;
            }
        }

        public async Task<int> UpdateCategory(int id, Category category)
        {
            using (var dbcontext = new EStoreContext())
            {
                var categoryToUpdate = await dbcontext.Categories.FindAsync(id);

                if (categoryToUpdate is null)
                {
                    throw new KeyNotFoundException("Category not found");
                }

                categoryToUpdate.CategoryName = category.CategoryName;

                await dbcontext.SaveChangesAsync();
                return categoryToUpdate.CategoryId;
            }
        }
    }
}
