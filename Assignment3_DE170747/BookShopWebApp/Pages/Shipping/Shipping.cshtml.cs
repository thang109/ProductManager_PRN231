using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace BookShopWebApp.Pages
{
    public class ShippingsModel : PageModel
    {
        private readonly ILogger<ShippingsModel> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public ShippingsModel(ILogger<ShippingsModel> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public List<Shippings> Shipping { get; set; }

        [BindProperty]
        public Shippings NewShip { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7251/odata/Shippings/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
           
                Shipping = JsonConvert.DeserializeObject<List<Shippings>>(jsonString);
            }
            else
            {
                Shipping = new List<Shippings>();
                ModelState.AddModelError(string.Empty, "Unable to load shipping.");
            }
        }

        
    }
}
