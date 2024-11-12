using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class EditBookModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditBookModel(IHttpClientFactory clientFactory)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(Book), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7251/odata/Books/{Book.BookId}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Books");
            }

            ModelState.AddModelError(string.Empty, "Unable to update book.");
            return Page();
        }
    }
}
