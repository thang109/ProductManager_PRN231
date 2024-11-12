using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class DeleteCategoryModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteCategoryModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Categories Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7251/odata/Categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Category = JsonConvert.DeserializeObject<Categories>(jsonString);
                return Page();
            }

            return RedirectToPage("./Categories");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7251/odata/Categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Categories");
            }

            ModelState.AddModelError(string.Empty, "Unable to delete category.");
            return Page();
        }
    }
}
