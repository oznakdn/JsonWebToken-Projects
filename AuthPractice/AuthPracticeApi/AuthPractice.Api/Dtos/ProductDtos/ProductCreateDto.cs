using System.ComponentModel.DataAnnotations;

namespace AuthPractice.Api.Dtos.ProductDtos;
public record ProductCreateDto([Required] string Name, decimal Price, int Quantity);


