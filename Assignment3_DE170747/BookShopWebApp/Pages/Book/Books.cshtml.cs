using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookShopBusiness; // Namespace chứa model Books
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace BookShopWebApp.Pages
{
    public class BooksModel : PageModel
    {
        private readonly ILogger<BooksModel> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public BooksModel(ILogger<BooksModel> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public List<Books> Books { get; set; }

        [BindProperty]
        public Books NewBook { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7251/odata/Books/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Books = JsonConvert.DeserializeObject<List<Books>>(jsonString);
            }
            else
            {
                Books = new List<Books>();
                ModelState.AddModelError(string.Empty, "Unable to load books.");
            }
        }

        
    }
}
