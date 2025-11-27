using InvoiceApp.Web.Models.Invoices;
using InvoiceApp.Web.Models.LookUps;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace InvoiceApp.Web.Controllers
{
    public class InvoiceController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("api");

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var token = HttpContext.Session.GetString("jwt");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var products = JsonConvert.DeserializeObject<List<ProductVm>>(
                await (await _httpClient.GetAsync("api/lookups/products")).Content.ReadAsStringAsync());

            var units = JsonConvert.DeserializeObject<List<UnitVm>>(
                await (await _httpClient.GetAsync("api/lookups/units")).Content.ReadAsStringAsync());

            var stores = JsonConvert.DeserializeObject<List<StoreVm>>(
                await (await _httpClient.GetAsync("api/lookups/stores")).Content.ReadAsStringAsync());

            ViewBag.Products = products;
            ViewBag.Units = units;
            ViewBag.Stores = stores;

            return View(new CreateInvoiceVm { InvoiceDate = DateTime.Now });
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceVm model)
        {
            var token = HttpContext.Session.GetString("jwt");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(model);
            var res = await _httpClient.PostAsync("api/invoices", new StringContent(json, Encoding.UTF8, "application/json"));

            if (!res.IsSuccessStatusCode)
                return BadRequest(await res.Content.ReadAsStringAsync());

            return Ok(new { success = true });
        }
    }
}
