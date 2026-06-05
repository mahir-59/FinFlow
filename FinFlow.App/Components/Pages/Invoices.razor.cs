namespace FinFlow.App.Components.Pages;

public partial class Invoices
{
    private List<InvoiceVm> InvoicesList =
    [
        new()
        {
            InvoiceNumber = "INV-1001",
            Description = "Due 2 days ago • ₹50,000",
            Status = "Overdue",
            BadgeClass = "ffin-badge-overdue",
            StatusLineClass = "ffin-line-overdue"
        },

        new()
        {
            InvoiceNumber = "INV-1002",
            Description = "Paid ₹30,000 of ₹50,000",
            Status = "Partial",
            BadgeClass = "ffin-badge-partial",
            StatusLineClass = "ffin-line-partial"
        },

        new()
        {
            InvoiceNumber = "INV-1003",
            Description = "Due in 12 days • ₹40,000",
            Status = "Unpaid",
            BadgeClass = "ffin-badge-unpaid",
            StatusLineClass = "ffin-line-unpaid"
        },

        new()
        {
            InvoiceNumber = "INV-1004",
            Description = "Due in 30 days • ₹1,100",
            Status = "Unpaid",
            BadgeClass = "ffin-badge-unpaid",
            StatusLineClass = "ffin-line-unpaid"
        },

        new()
        {
            InvoiceNumber = "INV-1005",
            Description = "Due in 45 days • ₹75,000",
            Status = "Unpaid",
            BadgeClass = "ffin-badge-unpaid",
            StatusLineClass = "ffin-line-unpaid"
        },

        new()
        {
            InvoiceNumber = "INV-1006",
            Description = "Due in 60 days • ₹90,000",
            Status = "Unpaid",
            BadgeClass = "ffin-badge-unpaid",
            StatusLineClass = "ffin-line-unpaid"
        }
    ];

    public class InvoiceVm
    {
        public string InvoiceNumber { get; set; } = "";

        public string Description { get; set; } = "";

        public string Status { get; set; } = "";

        public string BadgeClass { get; set; } = "";

        public string StatusLineClass { get; set; } = "";
    }
}
