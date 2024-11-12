using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using Newtonsoft.Json;
using System.Text;

namespace BookShopWebApp.Pages.Shipping
{
    public class ApproveModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApproveModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Shippings Shipping { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7251/odata/Shippings/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Shipping = JsonConvert.DeserializeObject<Shippings>(jsonString);
                return Page();
            }

            return RedirectToPage("./Shipping");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Shipping.Status = "isApprove";
            var client = _clientFactory.CreateClient();
            var jsonContent = new StringContent(JsonConvert.SerializeObject(Shipping), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7251/odata/Shippings/{Shipping.ShippingId}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Shipping/Shipping");
            }

            ModelState.AddModelError(string.Empty, "Unable to update book.");
            return Page();
        }
    }
}
