using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookShopWebApp.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public UsersModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<Users> Users { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7251/odata/Users/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<Users>>(jsonString);
            }
            else
            {
                Users = new List<Users>();
                ModelState.AddModelError(string.Empty, "Unable to load users.");
            }
        }
    }
}
