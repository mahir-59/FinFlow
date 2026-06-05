using Microsoft.AspNetCore.Components;

namespace FinFlow.App.Components.Pages;
public partial class LandingPage : ComponentBase
{
    private string CustomerName = "Ravi Distributors";

    private string TotalOutstanding = "₹ 140000";

    private int OpenInvoices = 3;

    private int OverdueInvoices = 1;

    private string ReminderText =
        "INV-1001 is due in 2 days.";

    public void NavigateToShop() => _navigationManager.NavigateTo("/shop");
    public void NavigateToInvoices() => _navigationManager.NavigateTo("/invoices");
}