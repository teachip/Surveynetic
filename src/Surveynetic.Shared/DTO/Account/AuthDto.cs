using System.Text.Json.Serialization;

namespace Surveynetic.Shared.DTO.Account
{
    public class AuthDto
    {
        [JsonPropertyName("token")] public string Token { get; set; }
        [JsonPropertyName("username")] public string UserName { get; set; }
        [JsonPropertyName("is_verified")] public bool IsVerified { get; set; }
    }
}
