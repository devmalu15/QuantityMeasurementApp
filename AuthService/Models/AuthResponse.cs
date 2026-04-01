namespace AuthService.Models;
public class AuthResponse    { public string Token { get; set; } = ""; public string Email { get; set; } = ""; public DateTime Expiry { get; set; } }