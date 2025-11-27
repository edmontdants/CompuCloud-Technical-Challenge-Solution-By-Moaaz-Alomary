
using InvoiceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class LookupsController(AppDbContext db) : ControllerBase
{
    [HttpGet("products")]
    [Authorize]
    public async Task<IActionResult> Products() =>
        Ok(await db.Products.Select(p => new { id = p.Id, name = p.Name }).ToListAsync());

    [HttpGet("units")]
    [Authorize]
    public async Task<IActionResult> Units() =>
        Ok(await db.Units.Select(u => new { id = u.Id, name = u.Name }).ToListAsync());

    [HttpGet("stores")]
    [Authorize]
    public async Task<IActionResult> Stores() =>
        Ok(await db.Stores.Select(s => new { id = s.Id, name = s.Name }).ToListAsync());
}
