using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.DTOs.Auth;

public record LoginRequest([EmailAddress] string Email, string Password);
