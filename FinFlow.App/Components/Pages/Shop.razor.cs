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
    private List<CartItemsResponse> _cartItems { get; set; } = new();

    private int TotalItems =>
        Products.Sum(x => x.NetQty);

    private decimal CartTotal =>
        Products.Sum(x => x.Price * x.NetQty);

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
        var random = new Random();
        int number = random.Next(100000, 1000000);
        product.NetQty++;
    }

    private void DecreaseQty(ItemResponse product)
    {
        if (product.NetQty > 0)
            product.NetQty--;
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