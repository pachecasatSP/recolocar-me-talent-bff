using re.colocar.me.talent.Domain.Authenticate;

namespace re.colocar.me.talent.Domain.Interfaces.Infrastructure;

public interface ILinkedinClient
{
    Task<LinkedinAuthToken> GetAuthToken(string Code);
    Task<LinkedinProfile> GetProfile(string token);
}