using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class AddCategoryModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public AddCategoryModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Categories NewCategory { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient();
            var jsonContent = new StringContent(JsonSerializer.Serialize(NewCategory), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7251/odata/Categories", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Categories");
            }

            ModelState.AddModelError(string.Empty, "Unable to add category.");
            return Page();
        }
    }
}
