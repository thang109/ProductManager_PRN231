using BookShopBusiness;
using System.Collections.Generic;

namespace BookShopRepository
{
    public interface IBooksRepository
    {
        List<Books> GetAllBooks();
        Books GetBookById(int id);
        void SaveBook(Books book);
        void UpdateBook(Books book);
        void DeleteBook(Books book);
    }
}
