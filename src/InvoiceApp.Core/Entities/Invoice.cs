namespace InvoiceApp.Core.Entities;

public class Invoice
{
    public int Id { get; set; }
    public string InvoiceNo { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public int StoreId { get; set; }
    public decimal Total { get; set; }
    public decimal Taxes { get; set; }
    public decimal Net { get; set; }
    int? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<InvoiceLine> Lines { get; set; } = new();
}
