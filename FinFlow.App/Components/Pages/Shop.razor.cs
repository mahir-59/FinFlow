using Microsoft.AspNetCore.Components;

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

    private List<ShopProductVm> Products =
    [
        new()
        {
            Id = 1,
            Name = "Namkeen Carton",
            Price = 400,
            Quantity = 2,
            ImageUrl = "images/namkeen.png"
        },

        new()
        {
            Id = 2,
            Name = "Biscuit Carton",
            Price = 300,
            Quantity = 1,
            ImageUrl = "images/biscuit.png"
        },

        new()
        {
            Id = 3,
            Name = "Juice Pack",
            Price = 250,
            Quantity = 0,
            ImageUrl = "images/juice.png"
        },

        new()
        {
            Id = 4,
            Name = "Soft Drinks",
            Price = 600,
            Quantity = 0,
            ImageUrl = "images/drinks.png"
        },
        new()
        {
            Id = 5,
            Name = "Wodka Bottles",
            Price = 6000,
            Quantity = 0,
            ImageUrl = "images/drinks.png"
        },
        new()
        {
            Id = 6,
            Name = "Alchohol Boxes",
            Price = 600,
            Quantity = 0,
            ImageUrl = "images/drinks.png"
        },
        new()
        {
            Id = 7,
            Name = "Beer Cases",
            Price = 9000,
            Quantity = 0,
            ImageUrl = "images/drinks.png"
        }
    ];

    private int TotalItems =>
        Products.Sum(x => x.Quantity);

    private decimal CartTotal =>
        Products.Sum(x => x.Price * x.Quantity);

    private void IncreaseQty(ShopProductVm product)
    {
        product.Quantity++;
    }

    private void DecreaseQty(ShopProductVm product)
    {
        if (product.Quantity > 0)
            product.Quantity--;
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

public class ShopProductVm
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}