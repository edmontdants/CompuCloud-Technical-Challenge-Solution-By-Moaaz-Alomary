namespace InvoiceApp.Web.Models.Invoices;

public class CreateInvoiceVm
{
    public string InvoiceNo { get; set; } = "";
    public DateTime InvoiceDate { get; set; }
    public int StoreId { get; set; }
    public List<CreateInvoiceLineVm> Lines { get; set; } = new();
    public decimal Total { get; set; }
    public decimal Taxes { get; set; }
    public decimal Net { get; set; }
}