using System.ComponentModel.DataAnnotations;

namespace Jwt.Api.Dtos.CustomerDtos;

public record CreateCustomerDto(
    [Required] 
    string FullName, 

    [Range(18,80)]
    int Age, 

    [Required] 
    [EmailAddress]
    string Email, 

    [Required]
    [Phone] 
    string Phone);
