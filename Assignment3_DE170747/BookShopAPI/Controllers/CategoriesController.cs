using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BookShopBusiness;
using BookShopRepository;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

namespace BookShopAPI.Controllers
{
    [Route("odata/[controller]")]
    public class CategoriesController : ODataController
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [EnableQuery]
        [HttpGet("GetAll")]
        public IActionResult GetCategory()
        {
            var categories = _categoriesRepository.GetAllCategories();
            return Ok(categories);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetCategoryById([FromODataUri] int id)
        {
            var categories = _categoriesRepository.GetCategoryById(id);
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult PostCategory([FromBody] Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _categoriesRepository.SaveCategory(categories);
            return Ok(categories);
        }

        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, [FromBody] Categories categories)
        {
            if (id != categories.CategoryID)
            {
                return BadRequest();
            }

            try
            {
                _categoriesRepository.UpdateCategory(categories);
            }
            catch
            {
                if (_categoriesRepository.GetCategoryById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content("Update Success");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromODataUri] int id)
        {
            var categories = _categoriesRepository.GetCategoryById(id);
            if (categories == null)
            {
                return NotFound();
            }

            _categoriesRepository.DeleteCategory(categories);
            return NoContent();
        }
    }
}
