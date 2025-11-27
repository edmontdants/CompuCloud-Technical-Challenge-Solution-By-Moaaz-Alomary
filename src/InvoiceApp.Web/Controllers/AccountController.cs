using InvoiceApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace InvoiceApp.Web.Controllers;

public class AccountController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("api");

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        var json = JsonConvert.SerializeObject(model);
        var response = await _httpClient.PostAsync("api/auth/login",
            new StringContent(json, Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "Invalid Credentials";
            return View();
        }

        var tokenResponse = JsonConvert.DeserializeObject<LoginResponse>(
            await response.Content.ReadAsStringAsync());

        HttpContext.Session.SetString("jwt", tokenResponse.Token);

        return RedirectToAction("Index", "Invoice");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("jwt");
        return RedirectToAction("Login");
    }
}
