using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class AddShippingModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public AddShippingModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Shippings NewShip { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            NewShip.Status = "Pending";
;            var client = _clientFactory.CreateClient();
            var jsonContent = new StringContent(JsonSerializer.Serialize(NewShip), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7251/odata/Shippings", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Shipping/Shipping");
            }

            ModelState.AddModelError(string.Empty, "Unable to add Shippings.");
            return Page();
        }
    }
}
