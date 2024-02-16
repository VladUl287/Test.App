namespace Test.Api.Configuration;

public sealed class CorsConfig
{
    public const string Position = "CorsConfig";

    public string[] Origins { get; init; } = Array.Empty<string>();
}
