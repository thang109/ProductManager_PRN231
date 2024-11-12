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
    public class ShippingsController : ODataController
    {
        private readonly IShippingsRepository _shippingsRepository;

        public ShippingsController(IShippingsRepository shippingsRepository)
        {
            _shippingsRepository = shippingsRepository;
        }

        [EnableQuery]
        [HttpGet("GetAll")]
        public IActionResult GetShipping()
        {
            var shippings = _shippingsRepository.GetAllShippings();
            return Ok(shippings);
        }

        [EnableQuery]
        [HttpGet("{id}")]

        public IActionResult GetShippingById([FromODataUri] int id)
        {
            var shippings = _shippingsRepository.GetShippingById(id);
            if (shippings == null)
            {
                return NotFound();
            }
            return Ok(shippings);
        }

        [HttpPost]
        public IActionResult PostShipping([FromBody] Shippings shippings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _shippingsRepository.SaveShipping(shippings);
            return Ok(shippings);
        }

        [HttpPut("{id}")]
        public IActionResult PutShipping(int id, [FromBody] Shippings shippings)
        {
            if (id != shippings.ShippingId)
            {
                return BadRequest();
            }

            try
            {
                _shippingsRepository.UpdateShipping(shippings);
            }
            catch
            {
                if (_shippingsRepository.GetShippingById(id) == null)
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
        public IActionResult DeleteShipping([FromODataUri] int id)
        {
            var shippings = _shippingsRepository.GetShippingById(id);
            if (shippings == null)
            {
                return NotFound();
            }

            _shippingsRepository.DeleteShipping(shippings);
            return NoContent();
        }
    }
}
