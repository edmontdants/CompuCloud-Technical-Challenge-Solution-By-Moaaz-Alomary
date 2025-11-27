namespace InvoiceApp.Web.Models.Invoices;

public class CreateInvoiceLineVm
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public decimal Net { get; set; }
}
