using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookShopWebApp.Pages
{
    public class CategoriesModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CategoriesModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

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
    }
}
