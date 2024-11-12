using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using Repository;
using EStoreAPI.Model;

namespace EStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController( ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok (await _categoryRepository.GetCategories());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            return Ok(await _categoryRepository.GetCategory(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryModel categoryModel)
        {

            Category category = new Category
            {
                CategoryName = categoryModel.CategoryName
            };
            return Ok(await _categoryRepository.UpdateCategory(id,category));
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory([FromBody]CategoryModel categoryModel)
        {
            try
            {
                var category = new Category
                {
                    CategoryName = categoryModel.CategoryName
                };
                var result = await _categoryRepository.AddCategory(category);
                if (result > 0)
                {
                    return Ok(category);
                }
                return BadRequest("Failed to add category.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
