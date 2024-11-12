using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class BuyModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public BuyModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Books Book { get; set; }

        [BindProperty]
        public Shippings Shipping { get; set; }

        public async Task OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7251/odata/Books/{id}");
            var uresponse = await client.GetAsync("https://localhost:7251/odata/Users/GetAll");
            if (uresponse.IsSuccessStatusCode)
            {
                var jsonString = await uresponse.Content.ReadAsStringAsync();
                List<Users> Users = JsonConvert.DeserializeObject<List<Users>>(jsonString);
            }
            else
            {
                List<Users> Users = new List<Users>();
                ModelState.AddModelError(string.Empty, "Unable to load users.");
            }
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Book = JsonConvert.DeserializeObject<Books>(jsonString);
            }
            else
            {
                Book = null;
                ModelState.AddModelError(string.Empty, "Book not found.");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Shipping.DateBooking = DateTime.Now;
            Shipping.Status = "Pending";
            var client = _clientFactory.CreateClient();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(Shipping), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7251/odata/Shippings", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Book/Books");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"Unable to create shipping. Server responded: {errorMessage}");
            }

            return Page();
        }
    }
}
