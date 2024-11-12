using EStoreAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace Assignment2.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public IEnumerable<ProductModelView> Products { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7226/");
                var response = await httpClient.GetAsync("api/Products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Products = JsonConvert.DeserializeObject<IEnumerable<ProductModelView>>(content);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostCreateOrder(int productId, int quantity)
        {
            var role = HttpContext.Session.GetInt32("ROLE");
            if (role == null)
            {
                return RedirectToPage("/Account/Login");
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7226/");
                    var response = await httpClient.GetAsync("api/Products/" + productId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductModelView>(content);
                        if (product != null)
                        {
                            var order = new OrderCreateModel
                            {
                                Order = new OrderModel
                                {
                                    MemberId = HttpContext.Session.GetInt32("AccountId"),
                                    OrderDate = null,
                                    RequiredDate = null,
                                    ShippedDate = null,
                                    Freight = (product.UnitPrice.Value * quantity).ToString()
                                },
                                ProductId = productId,
                                Quantity = quantity
                            };
                                                       
                            var jsonOrder = JsonConvert.SerializeObject(order);
                            var contentOrder = new StringContent(jsonOrder, System.Text.Encoding.UTF8, "application/json");

                            var postResponse = await httpClient.PostAsync("api/Orders", contentOrder);
                            if (postResponse.IsSuccessStatusCode)
                            {
                                return RedirectToPage("/Index"); 
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Failed to create order.");
                            }
                        }
                        else
                        {
                            return RedirectToPage("/Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }
            return Page(); 
        }


        public async Task<IActionResult> OnPostSearchProduct(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7226/");
                    var response = await httpClient.GetAsync("api/Products");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Products = JsonConvert.DeserializeObject<IEnumerable<ProductModelView>>(content);
                    }
                }
            }
            else
            {
                using(var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7226/");
                    var response = await httpClient.GetAsync("api/Products/Search/"+search);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Products = JsonConvert.DeserializeObject<IEnumerable<ProductModelView>>(content);
                    }
                }
            }
            return Page();
        }
    }
}
