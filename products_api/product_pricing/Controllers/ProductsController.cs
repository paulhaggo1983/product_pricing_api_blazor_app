namespace product_pricing.Controllers;
using Microsoft.AspNetCore.Mvc;
using product_pricing.Dtos;
using product_pricing.Services;




[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductPricingService _service;

    public ProductsController(ProductPricingService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<ProductListItemDto>> GetAll()
    {
        var result = _service.GetProducts()
            .Select(p => new ProductListItemDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.CurrentPrice,
                LastUpdated = p.LastUpdated
            })
            .ToList();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public ActionResult<ProductHistoryDto> GetHistory(int id)
    {
        var product = _service.GetProduct(id);
        if (product == null)
            return NotFound();

        return Ok(new ProductHistoryDto
        {
            Id = product.Id,
            Name = product.Name,
            PriceHistory = product.PriceHistory
                .Select(h => new PriceHistoryItemDto
                {
                    Price = h.Price,
                    Date = h.Date
                }).ToList()
        });
    }

    [HttpPost("{id:int}/apply-discount")]
    public ActionResult<ApplyDiscountDto> ApplyDiscount(int id, ApplyDiscountRequest request)
    {
        var product = _service.GetProduct(id);
        if (product == null)
            return NotFound();

        var (original, discounted) = _service.ApplyDiscount(product, request.DiscountPercentage);

        return Ok(new ApplyDiscountDto
        {
            Id = product.Id,
            Name = product.Name,
            OriginalPrice = original,
            DiscountedPrice = discounted
        });
    }

    [HttpPut("{id:int}/update-price")]
    public ActionResult<UpdatePriceDto> UpdatePrice(int id, UpdatePriceRequest request)
    {
        var product = _service.GetProduct(id);
        if (product == null)
            return NotFound();

        _service.UpdatePrice(product, request.NewPrice);

        return Ok(new UpdatePriceDto
        {
            Id = product.Id,
            Name = product.Name,
            NewPrice = product.CurrentPrice,
            LastUpdated = product.LastUpdated
        });
    }
}