using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            using(var context = new EStoreContext())
            {
                return await context.Products.ToListAsync();
            }
        }

        public async Task<Product?> GetProductById(int productID)
        {
            using(var context = new EStoreContext())
            {
                return await context.Products.FindAsync(productID);
            }
        }

        public async Task<int> AddProduct(Product product)
        {
            using(var context = new EStoreContext())
            {
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
                return product.ProductId;
            }
        }

        public async Task<int> UpdateProduct(int id, Product product)
        {
            using (var context = new EStoreContext())
            {
                var productToUpdate = await context.Products.FindAsync(id);

                if (productToUpdate is null)
                {
                    return -1; 
                }

                productToUpdate.ProductName = product.ProductName;
                productToUpdate.UnitPrice = product.UnitPrice;
                productToUpdate.UnitPrice = product.UnitPrice;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.UnitPrice = product.UnitPrice;
                productToUpdate.UnitInStock = product.UnitInStock;
                await context.SaveChangesAsync();

                return productToUpdate.ProductId;
            }
        }


        public async Task<int> DeleteProduct(int productID)
        {
            using (var context = new EStoreContext())
            {
                var productToDelete = context.Products.Find(productID);
                if (productToDelete is null)
                {
                    throw new Exception("Product not found");
                }
                context.Products.Remove(productToDelete);
                await context.SaveChangesAsync();
                return productID;
            }
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryID)
        {
            using(var context = new EStoreContext())
            {
                return await context.Products.Where(p => p.CategoryId == categoryID).ToListAsync();
            }
        }

        public async Task<List<Product>> GetProductsBySearch(string search)
        {
            using(var context = new EStoreContext())
            {
                return await context.Products.Where(p => p.ProductName.Contains(search)).ToListAsync();
            }
        }
    }
}
