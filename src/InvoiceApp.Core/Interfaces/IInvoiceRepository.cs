using InvoiceApp.Core.Entities;

namespace InvoiceApp.Core.Interfaces;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(int id);
    Task AddAsync(Invoice invoice);
    Task SaveChangesAsync();
}
