using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class DeleteBookModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteBookModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Books Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7251/odata/Books/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Book = JsonConvert.DeserializeObject<Books>(jsonString);
                return Page();
            }

            return RedirectToPage("./Books");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7251/odata/Books/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Books");
            }

            ModelState.AddModelError(string.Empty, "Unable to delete book.");
            return Page();
        }
    }
}
