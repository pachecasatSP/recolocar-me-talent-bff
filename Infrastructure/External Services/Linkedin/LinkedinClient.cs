using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using re.colocar.me.talent.Domain.Authenticate;
using re.colocar.me.talent.Domain.Interfaces.Infrastructure;

namespace re.colocar.me.talent.Infrastructure;
public class LinkedinClient : ILinkedinClient
{
    private LinkedinConfigurations _config;
    private ILogger<LinkedinClient> _logger;

    public LinkedinClient(IOptions<LinkedinConfigurations> config,
                          ILogger<LinkedinClient> logger)
    {
        _config = config.Value;
        _logger = logger;
    }

    public async Task<LinkedinAuthToken> GetAuthToken(string code)
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_config.BaseAddress);
        var contentRequest = new List<KeyValuePair<string, string>>
    {
        new("grant_type", "authorization_code"),
        new("code", code),
        new("client_id", _config.ClientId),
        new("client_secret", _config.Secret),
        new("redirect_uri", _config.RedirectUri)
    };

        var contentString = new FormUrlEncodedContent(contentRequest);
        contentString.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        var response = await httpClient.PostAsync(_config.GetAuthTokenPath, contentString);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<LinkedinAuthToken>(result) ?? new LinkedinAuthToken();
    }

    public async Task<LinkedinProfile> GetProfile(string token)
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_config.ApiBaseAddress ?? string.Empty);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync(_config.GetProfilePath);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<LinkedinProfile>(responseContent);
        if (result != null)
            result.AccessToken = token;

        return result;
    }
}