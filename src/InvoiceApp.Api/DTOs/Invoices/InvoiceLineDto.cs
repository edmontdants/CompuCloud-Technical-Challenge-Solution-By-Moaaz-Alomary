namespace InvoiceApp.Api.DTOs.Invoices;

public class InvoiceLineDto
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public decimal Price { get; set; }
    public decimal Qty { get; set; }
    public decimal Discount { get; set; }
    public decimal Net { get; set; }
}
