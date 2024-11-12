using BusinessLogic;
using DataAccess;

namespace Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product?> GetProductById(int productID);
        Task<int> AddProduct(Product product);
        Task<int> UpdateProduct(int id,Product product);
        Task<int> DeleteProduct(int productID);
        Task<List<Product>> GetProductsByCategory(int categoryID);
        Task<List<Product>> GetProductsBySearch(string search);
    }

    public class ProductRepository : IProductRepository
    {
        public async Task<int> AddProduct(Product product)
        {
            return await ProductDAO.Instance.AddProduct(product);
        }

        public Task<int> DeleteProduct(int productID)
        {
            return ProductDAO.Instance.DeleteProduct(productID);
        }

        public Task<Product?> GetProductById(int productID)
        {
            return ProductDAO.Instance.GetProductById(productID);
        }

        public Task<List<Product>> GetProducts()
        {
            return ProductDAO.Instance.GetProducts();
        }

        public Task<List<Product>> GetProductsByCategory(int categoryID)
        {
            return ProductDAO.Instance.GetProductsByCategory(categoryID);
        }

        public Task<List<Product>> GetProductsBySearch(string search)
        {
            return ProductDAO.Instance.GetProductsBySearch(search);
        }

        public Task<int> UpdateProduct(int id, Product product)
        {
            return ProductDAO.Instance.UpdateProduct(id,product);
        }
    }
}
