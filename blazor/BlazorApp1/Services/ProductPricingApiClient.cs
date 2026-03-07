using ProductPricing.Shared.Dtos;

namespace BlazorApp1.Services;

public class ProductPricingApiClient
{
    private readonly HttpClient _http;


    public ProductPricingApiClient(HttpClient http)
    {
        _http = http;
    }

    // Get all products
    public async Task<List<ProductListItemDto>> GetProductsAsync()
    {
        var products = await _http.GetFromJsonAsync<List<ProductListItemDto>>("api/products");

        return products ?? new List<ProductListItemDto>();
    }

    // Get price history for a product
    public async Task<ProductHistoryDto?> GetHistoryAsync(int id)
    {
        return await _http.GetFromJsonAsync<ProductHistoryDto>($"api/products/{id}");
    }

    // Apply a discount to a product
    public async Task<ApplyDiscountDto?> ApplyDiscountAsync(int id, decimal discountPercentage)
    {
        var response = await _http.PostAsJsonAsync(
            $"api/products/{id}/apply-discount",
            new ApplyDiscountRequest
            {
                DiscountPercentage = discountPercentage
            });

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ApplyDiscountDto>();
    }

    // Update the product price
    public async Task<UpdatePriceDto?> UpdatePriceAsync(int id, decimal newPrice)
    {
        var response = await _http.PutAsJsonAsync(
            $"api/products/{id}/update-price",
            new UpdatePriceRequest
            {
                NewPrice = newPrice
            });

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UpdatePriceDto>();
    }
}