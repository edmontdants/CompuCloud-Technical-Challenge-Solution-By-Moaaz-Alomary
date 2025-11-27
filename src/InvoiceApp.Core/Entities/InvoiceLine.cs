namespace InvoiceApp.Core.Entities;

public class InvoiceLine
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public decimal Price { get; set; }
    public decimal Qty { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public decimal Net { get; set; }
}
