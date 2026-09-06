namespace SalesManagementSystem.DTOs
{
    public class AddInvoiceDto
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public List<InvoiceDetailDto> Details { get; set; } = new();
    }

    public class InvoiceDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}