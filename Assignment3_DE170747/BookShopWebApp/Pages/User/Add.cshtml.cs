using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class AddUserModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public AddUserModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Users NewUser { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient();
            var jsonContent = new StringContent(JsonSerializer.Serialize(NewUser), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7251/odata/Users", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Users");
            }

            ModelState.AddModelError(string.Empty, "Unable to add book.");
            return Page();
        }
    }
}
