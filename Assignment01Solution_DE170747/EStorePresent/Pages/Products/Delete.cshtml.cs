using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EStoreAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace Assignment2.Pages.Products
{
    public class DeleteModel : PageModel
    {

        public DeleteModel()
        {
        }

        [BindProperty]
        public ProductModelView Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            if (type != 1)
            {
                return Unauthorized();
            }

            if (id == null )
            {
                return NotFound();
            }

            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7226/");

                var response = await httpClient.GetAsync("api/Products/" + id);
                string apiResponse = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ProductModelView>(apiResponse);
                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    Product = product;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            if (type != 1)
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7226/");

                var response = await httpClient.DeleteAsync("api/Products/" + id);
            }

            return RedirectToPage("./Index");
        }
    }
}
