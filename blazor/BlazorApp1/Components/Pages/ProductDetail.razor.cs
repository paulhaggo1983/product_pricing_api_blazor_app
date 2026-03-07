using Microsoft.AspNetCore.Components;
using ProductPricing.Shared.Dtos;
using System.Globalization;

namespace BlazorApp1.Components.Pages; // change to match your folder/namespace

public partial class ProductDetailBase : ComponentBase
{
    [Inject] public BlazorApp1.Services.ProductPricingApiClient Api { get; set; } = default!;

    [Parameter] public int ProductId { get; set; }

    protected ProductHistoryDto? history;

    protected decimal? newDiscount;
    protected decimal? newPrice;

    protected string? discountResult;
    protected string? priceResult;

    protected decimal? previewDiscountedPrice;

    protected override async Task OnInitializedAsync()
    {
        await ReloadHistory();
        StateHasChanged();
    }

    protected async Task Reload()
    {
        await ReloadHistory();
        StateHasChanged();
    }

    private async Task ReloadHistory()
    {
        history = await Api.GetHistoryAsync(ProductId);
    }

    protected async Task PreviewDiscount()
    {
        discountResult = null;
        previewDiscountedPrice = null;

        if (newDiscount is null || newDiscount < 0 || newDiscount > 100)
        {
            discountResult = "Discount must be between 0 and 100.";
            StateHasChanged();
            return;
        }

        var result = await Api.ApplyDiscountAsync(ProductId, newDiscount.Value);
        if (result == null)
        {
            discountResult = "Discount calculation failed.";
            StateHasChanged();
            return;
        }

        previewDiscountedPrice = result.DiscountedPrice;

        discountResult =
            $"Original: {result.OriginalPrice.ToString("C", new CultureInfo("en-GB"))}, " +
            $"Preview: <b>{result.DiscountedPrice.ToString("C", new CultureInfo("en-GB"))}</b>"; ;

        StateHasChanged();
    }

    protected async Task ApplyDiscountUpdate()
    {
        priceResult = null;

        if (previewDiscountedPrice == null)
            return;

        var result = await Api.UpdatePriceAsync(ProductId, previewDiscountedPrice.Value);
        if (result == null)
        {
            priceResult = "Update price failed.";
            StateHasChanged();
            return;
        }

        previewDiscountedPrice = null;
        priceResult = $"Updated to {result.NewPrice.ToString("C", new CultureInfo("en-GB"))}";

        await ReloadHistory();
        StateHasChanged();
    }

    protected async Task UpdatePrice()
    {
        priceResult = null;

        if (newPrice is null)
        {
            priceResult = "Enter a new price.";
            StateHasChanged();
            return;
        }

        if (newPrice <= 0)
        {
            priceResult = "Price must be greater than 0.";
            StateHasChanged();
            return;
        }

        var result = await Api.UpdatePriceAsync(ProductId, newPrice.Value);
        if (result == null)
        {
            priceResult = "Update price failed.";
            StateHasChanged();
            return;
        }

        previewDiscountedPrice = null;
        priceResult = $"Updated to {result.NewPrice.ToString("C", new CultureInfo("en-GB"))}";

        await ReloadHistory();
        StateHasChanged();
    }
}