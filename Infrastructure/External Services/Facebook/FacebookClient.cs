using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using re.colocar.me.talent.Domain.Authenticate.Facebook;
using re.colocar.me.talent.Domain.Interfaces.Infrastructure;

namespace re.colocar.me.talent.Infrastructure;

public class FacebookClient : IFacebookClient
{
    private readonly FacebookConfigurations _config;
    private readonly ILogger<FacebookClient> _logger;

    public FacebookClient(IOptions<FacebookConfigurations> config,
                          ILogger<FacebookClient> logger) 
    {
        _config = config.Value;
        _logger = logger;
    }

    public async Task<FacebookProfile> GetFacebookProfileAsync(string authToken)
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_config.BaseAddress ?? string.Empty);

        var response = await httpClient.GetAsync($"{_config.GetProfilePath}?fields=id,email,name,picture&access_token={authToken}&method=get&pretty=0&sdk=joey&suppress_http_code=1");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<FacebookProfile>(responseContent);
        if (result != null)
            result.AccessToken = authToken;

        return result;
    }

}
