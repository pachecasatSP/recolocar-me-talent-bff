namespace re.colocar.me.talent.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ProfileEntity> AuthenticateAsync(AuthRequest request);
    }
}
