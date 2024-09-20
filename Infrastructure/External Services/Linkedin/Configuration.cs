public class LinkedinConfigurations
{

    public const string LinkedinOptions = "LinkedinOptions";

    public string BaseAddress { get; set; } = string.Empty;
    public string GetAuthTokenPath { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string RedirectUri { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public string? GetProfilePath { get;  set; }
    public string? ApiBaseAddress { get; set; }
}