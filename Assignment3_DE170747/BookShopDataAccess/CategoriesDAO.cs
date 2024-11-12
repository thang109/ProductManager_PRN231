using BookShopBusiness;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookShopDataAccess
{
    public class CategoriesDAO
    {
        public static List<Categories> GetCategories()
        {
            using (var context = new MyDbContext())
            {
                return context.Categories.ToList();
            }
        }

        public static Categories GetCategoryById(int id)
        {
            using (var context = new MyDbContext())
            {
                return context.Categories.FirstOrDefault(c => c.CategoryID == id);
            }
        }

        public static void InsertCategory(Categories category)
        {
            using (var context = new MyDbContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public static void UpdateCategory(Categories category)
        {
            using (var context = new MyDbContext())
            {
                context.Categories.Update(category);
                context.SaveChanges();
            }
        }

        public static void DeleteCategory(Categories category)
        {
            using (var context = new MyDbContext())
            {
                var listBooks = context.Books.Where(b => b.CategoryId == category.CategoryID).ToList();
                foreach (var book in listBooks)
                {
                    var listShipping = context.Shippings.Where(s => s.BookId == book.BookId).ToList();
                    foreach (var shipping in listShipping)
                    {
                        context.Shippings.Remove(shipping);
                    }
                    context.Books.Remove(book);
                }
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
