using System.Text.Json.Serialization;

namespace Infinit.Assessment;

public class GithubBlobResponse
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;

    [JsonPropertyName("encoding")]
    public string Encoding { get; set; } = default!;

    [JsonPropertyName("url")]
    public Uri Url { get; set; } = default!;

    [JsonPropertyName("sha")]
    public string Sha { get; set; } = default!;

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("node_id")]
    public string NodeId { get; set; } = default!;
}
