using System.ComponentModel.DataAnnotations;

namespace Jwt.Api.Dtos.UserDtos;

public record UserRoleAssignDto(int RoleId, int UserId);