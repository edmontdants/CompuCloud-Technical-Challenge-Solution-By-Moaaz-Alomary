using InvoiceApp.Core.Entities;
using InvoiceApp.Core.Interfaces;
using InvoiceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _db;
    public InvoiceRepository(AppDbContext db) { _db = db; }

    public async Task AddAsync(Invoice invoice)
    {
        await _db.Invoices.AddAsync(invoice);
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        return await _db.Invoices.Include(i => i.Lines).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}
