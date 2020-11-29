using System.Text.Json.Serialization;

namespace Surveynetic.Shared.DTO.Account
{
    public class RegisterDto
    {
        [JsonPropertyName("username")] public string UserName { get; set; }
        [JsonPropertyName("email")] public string Email { get; set; }
        [JsonPropertyName("password")] public string Password { get; set; }
    }
}
