using System.Net.Http.Json;
using ProductPricing.Shared.Dtos;

namespace BlazorApp1.Services;

public class ProductPricingApiClient
{
    private readonly HttpClient _http;

    public ProductPricingApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProductListItemDto>> GetProductsAsync()
    {
        // Matches your API route: /api/products
        var result = await _http.GetFromJsonAsync<List<ProductListItemDto>>("api/products");
        return result ?? new List<ProductListItemDto>();
    }

    public async Task<ProductHistoryDto?> GetProductHistoryAsync(int id)
    {
        return await _http.GetFromJsonAsync<ProductHistoryDto>($"api/products/{id}");
    }

    public async Task<ApplyDiscountDto?> ApplyDiscountAsync(int id, decimal discountPercentage)
    {
        var res = await _http.PostAsJsonAsync($"api/products/{id}/apply-discount",
            new ApplyDiscountRequest { DiscountPercentage = discountPercentage });

        if (!res.IsSuccessStatusCode) return null;

        return await res.Content.ReadFromJsonAsync<ApplyDiscountDto>();
    }

    public async Task<UpdatePriceDto?> UpdatePriceAsync(int id, decimal newPrice)
    {
        var res = await _http.PutAsJsonAsync($"api/products/{id}/update-price",
            new UpdatePriceRequest { NewPrice = newPrice });

        if (!res.IsSuccessStatusCode) return null;

        return await res.Content.ReadFromJsonAsync<UpdatePriceDto>();
    }
}