using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class EditCategoryModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditCategoryModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Categories Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7251/odata/Categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Categories = JsonConvert.DeserializeObject<Categories>(jsonString);
                return Page();
            }

            return RedirectToPage("./Categories");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(Categories), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7251/odata/Categories/{Categories.CategoryID}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Categories");
            }

            ModelState.AddModelError(string.Empty, "Unable to update category.");
            return Page();
        }
    }
}
