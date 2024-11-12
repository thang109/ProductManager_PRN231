using BookShopBusiness;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookShopWebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7251/");
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            ErrorMessage = null;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var loginData = new { Username = Username, Password = Password };
            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("odata/Users/Authenticate", content);

            if (response.IsSuccessStatusCode)
            {
                var userJson = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Users>(userJson);

                if (user != null)
                {
                    HttpContext.Session.SetString("Username", JsonConvert.SerializeObject(user.UserName));
                    HttpContext.Session.SetInt32("UserID", user.UserId);
                    Response.Cookies.Append("UserID", user.UserId.ToString(), new CookieOptions
                    {
                        Expires = DateTimeOffset.Now.AddMinutes(60)
                    });
                    return RedirectToPage("/Index");
                }
            }

            ErrorMessage = "Invalid username or password.";
            return Page();
        }
    }
}
