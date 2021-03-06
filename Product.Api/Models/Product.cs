using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Product.Api.Models;

public class ProductDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [PositiveNumber(ErrorMessage = "Price must be positive;")]
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    [PositiveNumber(ErrorMessage = "Quantity must be positive;")]
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }

}