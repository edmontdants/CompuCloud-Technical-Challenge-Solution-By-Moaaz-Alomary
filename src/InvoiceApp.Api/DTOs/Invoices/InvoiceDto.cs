namespace InvoiceApp.Api.DTOs.Invoices;

public class InvoiceDto
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal Net { get; set; }
    public List<InvoiceLineDto> Items { get; set; } = new();
}
