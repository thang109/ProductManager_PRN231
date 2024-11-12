using BookShopBusiness;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookShopDataAccess
{
    public class BooksDAO
    {
        public static List<Books> GetBooks()
        {
            using (var context = new MyDbContext())
            {
                return context.Books.Include(b => b.Category).ToList();
            }
        }

        public static Books GetBookById(int id)
        {
            using (var context = new MyDbContext())
            {
                return context.Books.Include(b => b.Category)
                                    .FirstOrDefault(b => b.BookId == id);
            }
        }

        public static void InsertBook(Books book)
        {
            using (var context = new MyDbContext())
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
        }

        public static void UpdateBook(Books book)
        {
            using (var context = new MyDbContext())
            {
                context.Books.Update(book);
                context.SaveChanges();
            }
        }

        public static void DeleteBook(Books book)
        {
            using (var context = new MyDbContext())
            {
                context.Books.Remove(book);
                context.SaveChanges();
            }
        }
    }
}
