using EStoreAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EStorePresent.Pages.Member
{
    public class IndexModel : PageModel
    {
        public IList<MemberModel> members { get; set; }
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
                var response = await httpClient.GetAsync("api/Members");
                string apiResponse = await response.Content.ReadAsStringAsync();
                members = JsonConvert.DeserializeObject<List<MemberModel>>(apiResponse);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int memberId)
        {
            int type = HttpContext.Session.GetInt32("ROLE") == null ? -1 : (int)HttpContext.Session.GetInt32("ROLE");
            if (type != 1)
            {
                return Unauthorized();
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7226/");
                var response = await httpClient.DeleteAsync($"api/Members/{memberId}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage(); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to delete member. Please try again.");
                    return Page();
                }
            }
        }
    }
}
