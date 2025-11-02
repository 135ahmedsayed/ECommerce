using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs.Auth;

public record RegisterRequest([EmailAddress] string Email, string DisplayName, string Password,
    string? UserName = "MMM", string? PhoneNumber = "");
