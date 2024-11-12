using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class EditUserModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditUserModel(IHttpClientFactory clientFactory)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7251/odata/Users/{User.UserId}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Users");
            }

            ModelState.AddModelError(string.Empty, "Unable to update user.");
            return Page();
        }
    }
}
