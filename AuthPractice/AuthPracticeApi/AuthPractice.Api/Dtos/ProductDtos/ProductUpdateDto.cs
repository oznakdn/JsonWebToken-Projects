using System.ComponentModel.DataAnnotations;

namespace AuthPractice.Api.Dtos.ProductDtos;


public record ProductUpdateDto([Required] string Name, decimal Price, int Quantity);

