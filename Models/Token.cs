using System.Text.Json.Serialization;

namespace inventoryeyeback;

public class Token
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
}