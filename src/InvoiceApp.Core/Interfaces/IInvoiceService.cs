using InvoiceApp.Core.Entities;

namespace InvoiceApp.Core.Interfaces;

public interface IInvoiceService
{
    Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(int id);
}
