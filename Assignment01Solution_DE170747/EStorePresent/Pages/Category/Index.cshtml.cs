using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EStoreAPI.Model;
using Newtonsoft.Json;
using System.Text;

namespace Assignment2.Pages.Category
{
    public class IndexModel : PageModel
    {

        public IndexModel()
        {
        }

        public IList<CategoryModel> Categories { get; set; }
        public string ErrorMessage { get; set; }

        [BindProperty]
        public CategoryModel Category { get; set; }

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

                var response = await httpClient.GetAsync("api/Categories");
                string apiResponse = await response.Content.ReadAsStringAsync();
                Categories = JsonConvert.DeserializeObject<List<CategoryModel>>(apiResponse);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            if (type != 1)
            {
                return Unauthorized();
            }

            string action = Request.Form["Action"];
            if (action == "CREATE")
            {
                var newCategory = new CategoryModel
                {
                    CategoryName = Category.CategoryName
                };
                using(var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7226/");

                    var jsonContent = JsonConvert.SerializeObject(newCategory);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("api/Categories", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<CategoryModel>(data);

                        if (result == null)
                        {
                            ErrorMessage = "Can not add category";
                            return Page();
                        }

                        return RedirectToPage("/Category/Index");
                    }
                    else
                    {
                        ErrorMessage = "Failed to add category. Please try again.";
                        return Page();
                    }
                }
            }

            if (action == "EDIT")
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7226/");
                    var jsonContent = JsonConvert.SerializeObject(Category);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync($"api/Categories/{Category.CategoryId}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToPage("/Category/Index");
                    }
                    else
                    {
                        ErrorMessage = "Failed to edit category. Please try again.";
                        return Page();
                    }
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
