using InvoiceApp.Core.Interfaces;
using InvoiceApp.Core.Entities;

namespace InvoiceApp.Infrastructure.Services;

public class InvoiceService(IInvoiceRepository repo) : IInvoiceService
{
    public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
    {
        // business rules: calculate totals from lines
        decimal total = 0m;
        foreach (var line in invoice.Lines)
        {
            line.Total = line.Price * line.Qty;
            line.Net = line.Total - line.Discount;
            total += line.Net;
        }
        invoice.Total = total;
        // simple tax calc, assume Taxes field already provided or compute 0
        invoice.Net = invoice.Total + invoice.Taxes;


        await repo.AddAsync(invoice);
        await repo.SaveChangesAsync();
        return invoice;
    }

    public async Task<Invoice?> GetByIdAsync(int id) => await repo.GetByIdAsync(id);
}
