using EStoreAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Assignment2.Pages.Products
{
    public class IndexModel : PageModel
    {

        public IndexModel()
        {
        }

        public IList<ProductModelView> Products { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            if (type != 1)
            {
                return Unauthorized();
            }
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7226/");

                var response = await httpClient.GetAsync("api/Products");
                string apiResponse = await response.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<List<ProductModelView>>(apiResponse);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7226/");

                var response = await httpClient.DeleteAsync("api/Products/" + id);
            }
            return RedirectToPage("./Index");
        }
    }
}
