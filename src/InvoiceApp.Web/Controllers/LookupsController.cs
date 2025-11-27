using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace InvoiceApp.Web.Controllers
{
    public class LookupsController(IHttpClientFactory factory) : Controller
    {
        private readonly HttpClient _httpClient = factory.CreateClient("api");

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var token = HttpContext.Session.GetString("jwt");

            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = await _httpClient.GetAsync("api/lookups/products");
            var json = await resp.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> Units()
        {
            var token = HttpContext.Session.GetString("jwt");

            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = await _httpClient.GetAsync("api/lookups/units");
            var json = await resp.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> Stores()
        {
            var token = HttpContext.Session.GetString("jwt");

            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = await _httpClient.GetAsync("api/lookups/stores");
            var json = await resp.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }
    }
}
