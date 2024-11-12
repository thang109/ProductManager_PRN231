using EStoreAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Assignment2.Pages.Products
{
    public class AddProductModel : PageModel
    {
        public AddProductModel()
        {
        }

        [BindProperty]
        public string ProductName { get; set; }

        [BindProperty]
        public int CategoryId { get; set; }

        [BindProperty]
        public int Weight { get; set; }

        [BindProperty]
        public decimal UnitPrice { get; set; }

        [BindProperty]
        public int UnitInStock { get; set; }

        public IEnumerable<ProductModelView> Products { get; set; }
        public IEnumerable<CategoryModel> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7226/");

                var response = await httpClient.GetAsync("api/Categories");
                string apiResponse = await response.Content.ReadAsStringAsync();
                Categories = JsonConvert.DeserializeObject<List<CategoryModel>>(apiResponse);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7226/");

                    var response = await httpClient.GetAsync("api/Categories");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Categories = JsonConvert.DeserializeObject<List<CategoryModel>>(apiResponse);
                }
                return Page();
            }

            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7226/");

                var product = new ProductModelView
                {
                    ProductName = ProductName,
                    CategoryId = CategoryId,
                    Weight = Weight,
                    UnitPrice = UnitPrice,
                    UnitInStock = UnitInStock
                };

                var json = JsonConvert.SerializeObject(product);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/Products", data);
            }

            return RedirectToPage("./Index");
        }
    }
}
