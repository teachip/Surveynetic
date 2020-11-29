using System.Text.Json.Serialization;

namespace Surveynetic.Shared.DTO.Account
{
    public class LoginDto
    {
        [JsonPropertyName("email")] public string Email { get; set; }
        [JsonPropertyName("password")] public string Password { get; set; }
    }
}
