using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using EStoreAPI.Model;

namespace Assignment2.Pages.Account
{
    public class SignUpModel : PageModel
    {

        [BindProperty]
        public AccountSignUpModel Account { get; set; } = new AccountSignUpModel();

        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Account.Email == null || Account.CompanyName == null || Account.City == null || Account.Country == null || Account.Password == null || Account.ConfirmPassword == null)
            {
                ErrorMessage = "Please fill in all fields";
                return Page();
            }

            if (ModelState.IsValid)
            {
                if (Account.Password != Account.ConfirmPassword)
                {
                    ErrorMessage = "Password and Confirm Password do not match";
                    return Page();
                }

                var newMember = new MemberModel
                {
                    Email = Account.Email,
                    CompanyName = Account.CompanyName,
                    City = Account.City,
                    Country = Account.Country,
                    Password = Account.Password
                };

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7226/");

                    var jsonContent = JsonConvert.SerializeObject(newMember);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("api/Members", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<MemberModel>(data);

                        if (result == null)
                        {
                            ErrorMessage = "Email already exists";
                            return Page();
                        }

                        return RedirectToPage("/Account/Login");
                    }
                    else
                    {
                        ErrorMessage = "Failed to add member. Please try again.";
                        return Page();
                    }
                }
            }
            return Page();
        }

        public class AccountSignUpModel
        {
            [Required]
            [StringLength(50)]
            public string? Email { get; set; }

            [Required]
            [StringLength(50, MinimumLength = 3)]
            public string? CompanyName { get; set; }

            [Required]
            [StringLength(50, MinimumLength = 3)]
            public string? City { get; set; }

            [Required]
            [StringLength(50, MinimumLength = 3)]
            public string? Country { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 3)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
