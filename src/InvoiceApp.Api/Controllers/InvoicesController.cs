using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Core.Entities;
using InvoiceApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace InvoiceApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _service;
    public InvoicesController(IInvoiceService service) { _service = service; }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] Invoice invoice)
    {
        var created = await _service.CreateInvoiceAsync(invoice);
        return CreatedAtAction(nameof(Create), new { id = created.Id }, created);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var inv = await _service.GetByIdAsync(id);
        if (inv == null) return NotFound();
        return Ok(inv);
    }
}
