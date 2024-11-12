using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using Repository;
using EStoreAPI.Model;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productRepository.GetProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return Ok(await _productRepository.GetProductById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductModel productModel)
        {
            if (id != productModel.ProductId)
            {
                return BadRequest();
            }
            var product = new Product
            {
                CategoryId = productModel.CategoryId,
                ProductName = productModel.ProductName,
                Weight = productModel.Weight,
                UnitPrice = productModel.UnitPrice,
                UnitInStock = productModel.UnitInStock
            };  
            return Ok(await _productRepository.UpdateProduct(id,product));
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductModel productModel)
        {
            var product = new Product
            {
                CategoryId = productModel.CategoryId,
                ProductName = productModel.ProductName,
                Weight = productModel.Weight,
                UnitPrice = productModel.UnitPrice,
                UnitInStock = productModel.UnitInStock
            };
            return Ok(await _productRepository.AddProduct(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
        {
            return Ok(await _productRepository.GetProductsByCategory(categoryId));
        }

        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string search)
        {
            return Ok(await _productRepository.GetProductsBySearch(search));
        }
    }
}
