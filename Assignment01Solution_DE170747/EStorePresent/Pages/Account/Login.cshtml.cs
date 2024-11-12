using EStoreAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Newtonsoft.Json;
using EStorePresent.Model;

namespace Assignment2.Pages.Account
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public AccountModelLogin Account { get; set; } = new AccountModelLogin();

        public string ErrorMessage { get; set; }

        // Handle GET requests
        public IActionResult OnGet()
        {
            return Page();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Account.UserName is null || Account.Password is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            if (Account.UserName == "admin" && Account.Password == "admin")
            {
                HttpContext.Session.SetString("UserName", "admin");
                HttpContext.Session.SetInt32("AccountId", 0);
                HttpContext.Session.SetInt32("ROLE", 1);
                return RedirectToPage("/Index");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7226/");

                var loginData = new MemberLoginModel
                {
                    Email = Account.UserName,
                    Password = Account.Password
                };
                var jsonContent = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/Members/login", content);

                var data1 = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var account = JsonConvert.DeserializeObject<MemberModel>(data);
                    if(account == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                        return Page();
                    }

                    HttpContext.Session.SetString("UserName", account.Email);
                    HttpContext.Session.SetInt32("AccountId", account.MemberId);
                    HttpContext.Session.SetInt32("ROLE", 0);

                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return Page();
                }
            }
        }

        public class AccountModelLogin
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
