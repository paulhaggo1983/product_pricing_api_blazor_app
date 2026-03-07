using Microsoft.AspNetCore.Components;
using ProductPricing.Shared.Dtos;
using Radzen;

namespace BlazorApp1.Components.Pages; // adjust if needed

public partial class ProductListBase : ComponentBase
{
    [Inject] public BlazorApp1.Services.ProductPricingApiClient Api { get; set; } = default!;
    [Inject] public DialogService DialogService { get; set; } = default!;

    protected List<ProductListItemDto>? products;

    protected override async Task OnInitializedAsync()
    {
        products = await Api.GetProductsAsync();
        StateHasChanged();
    }

    protected async Task OpenHistory(int productId)
    {
        await DialogService.OpenAsync<ProductDetail>(
            $"Product {productId} History",
            new Dictionary<string, object> { { "ProductId", productId } },
            new DialogOptions
            {
                Width = "60vw",
                Height = "78vh",
                Resizable = true,
                Draggable = true
            }
        );

        products = await Api.GetProductsAsync();
        StateHasChanged();
    }
}