using BookShopBusiness;
using BookShopDataAccess;
using System.Collections.Generic;

namespace BookShopRepository
{
    public class BooksRepository : IBooksRepository
    {
        public void DeleteBook(Books book) => BooksDAO.DeleteBook(book);

        public void SaveBook(Books book) => BooksDAO.InsertBook(book);

        public void UpdateBook(Books book) => BooksDAO.UpdateBook(book);

        public List<Books> GetAllBooks() => BooksDAO.GetBooks();

        public Books GetBookById(int id) => BooksDAO.GetBookById(id);
    }
}
