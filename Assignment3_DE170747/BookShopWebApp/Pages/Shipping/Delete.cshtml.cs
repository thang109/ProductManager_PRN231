using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class DeleteShippingModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteShippingModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Shippings Shipping { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7251/odata/Shippings/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Shipping = JsonConvert.DeserializeObject<Shippings>(jsonString);
                return Page();
            }

            return RedirectToPage("./Shipping");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7251/odata/Shippings/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Shipping");
            }

            ModelState.AddModelError(string.Empty, "Unable to delete book.");
            return Page();
        }
    }
}
