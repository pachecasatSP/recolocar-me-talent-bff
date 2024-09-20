using re.colocar.me.talent.Domain.Authenticate.Facebook;

namespace re.colocar.me.talent.Domain.Interfaces.Infrastructure
{
    public interface IFacebookClient
    {
        Task<FacebookProfile> GetFacebookProfileAsync(string authToken);
    }
}
