using System.Text.Json.Serialization;

namespace Infinit.Assessment.Github.Responses;

public class GithubTreeResponse
{
    [JsonPropertyName("sha")]
    public string Sha { get; set; } = default!;

    [JsonPropertyName("url")]
    public Uri Url { get; set; } = default!;

    [JsonPropertyName("tree")]
    public List<GithubTreeNode> Tree { get; set; } = default!;
}

public class GithubTreeNode
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = default!;

    [JsonPropertyName("mode")]
    public string Mode { get; set; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("sha")]
    public string Sha { get; set; } = default!;

    [JsonPropertyName("size")]
    public long Size { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; } = default!;
}
