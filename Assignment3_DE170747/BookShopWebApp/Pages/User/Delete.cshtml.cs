using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class DeleteUserModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteUserModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Users User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7251/odata/Users/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                User = JsonConvert.DeserializeObject<Users>(jsonString);
                return Page();
            }

            return RedirectToPage("./Users");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7251/odata/Users/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Users");
            }

            ModelState.AddModelError(string.Empty, "Unable to delete book.");
            return Page();
        }
    }
}
