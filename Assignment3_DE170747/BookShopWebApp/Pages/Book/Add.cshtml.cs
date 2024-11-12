using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Text;
using Newtonsoft.Json;

namespace BookShopWebApp.Pages
{
    public class AddBookModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public AddBookModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Books NewBook { get; set; }

        public List<Categories> Categories { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7251/odata/Categories/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Categories = JsonConvert.DeserializeObject<List<Categories>>(jsonString);
            }
            else
            {
                Categories = new List<Categories>();
                ModelState.AddModelError(string.Empty, "Unable to load categories.");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(NewBook), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7251/odata/Books", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Books");
            }

            ModelState.AddModelError(string.Empty, "Unable to add book.");
            return Page();
        }
    }
}
