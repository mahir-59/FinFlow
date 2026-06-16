using Microsoft.AspNetCore.Components;
using FinFlow.Modules.Items.Items.Model.Classes;
namespace FinFlow.App.Components.Pages;

public partial class Shop : ComponentBase
{
    private string SelectedCategory = "All";

    public void PlaceOrder() => _navigationManager.NavigateTo("/order-confirmed");

    private List<string> Categories =
    [
        "All",
        "Snacks",
        "Beverages",
        "Bakery"
    ];

    private List<ItemResponse> Products { get; set; } = new();

    private int TotalItems =>
        Products.Sum(x => x.StockQuantity);

    private decimal CartTotal =>
        Products.Sum(x => x.Price * x.StockQuantity);

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _loaderService.Show();
            Products = await _itemViewModel.GetAllItems();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading products: {ex.Message}");
        }
        finally
        {
            _loaderService.Hide();
        }
    }

    private void IncreaseQty(ItemResponse product)
    {
        product.StockQuantity++;
    }

    private void DecreaseQty(ItemResponse product)
    {
        if (product.StockQuantity > 0)
            product.StockQuantity--;
    }

    private void SelectCategory(string category)
    {
        SelectedCategory = category;
    }
    private bool IsCartOpen;

    private void OpenCart()
    {
        IsCartOpen = true;
    }

    private void CloseCart()
    {
        IsCartOpen = false;
    }
}