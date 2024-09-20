using Microsoft.Extensions.Caching.Memory;
using re.colocar.me.talent.Domain.Interfaces.Infrastructure;
using re.colocar.me.talent.Domain.Interfaces.Repositories;
using re.colocar.me.talent.Domain.Interfaces.Services;

namespace re.colocar.me.talent.Domain
{
    public class AuthenticationService : IAuthenticationService
    {
        private ILinkedinClient _linkedinClient;
        private IFacebookClient _facebookClient;
        private IUserRepository _repository;
        private IMemoryCache _cache;

        public AuthenticationService(ILinkedinClient linkedinClient,
                                     IFacebookClient facebookClient,
                                     IUserRepository repository,
                                     IMemoryCache cache)
        {
            _linkedinClient = linkedinClient;
            _facebookClient = facebookClient;
            _repository = repository;
            _cache = cache;
        }

        public async Task<ProfileEntity> AuthenticateAsync(AuthRequest request)
        {
            var profile = request.Source switch
            {
                Constants.Linkedin => await GetProfileFromLinkedin(request.Code),
                Constants.Facebook => await GetProfileFromFacebook(request.Code),
                _ => null
            };

            if (profile is null)
                return null;

            var systemUser = _repository.GetByUserName(profile.Email!);
            if (systemUser == null)
            {
                systemUser = _repository.Save(profile);
            }
            profile.Id = systemUser.Id;

             return profile;
        }

        private async Task<ProfileEntity> GetProfileFromFacebook(string code)
        {            
            var profile = await _facebookClient.GetFacebookProfileAsync(code);

            return new ProfileEntity()
            {
                Name = new Name(profile.Name, string.Empty),
                Email = profile.Email,
                UserName = profile.Email,
                Source = Constants.Facebook,
                PictureUrl = profile.Picture?.Data!.Url!,
                PlatformId = profile.Id
            };
        }

        private async Task<ProfileEntity> GetProfileFromLinkedin(string code)
        {               
            var authData = await _linkedinClient.GetAuthToken(code) ?? throw new ArgumentException("Code is invalid.");
            var profile = await _linkedinClient.GetProfile(authData.AccessToken!) ?? throw new ArgumentException("Token is invalid.");

            return new ProfileEntity()
            {
                Name = new Name(profile.Given_name, profile.Family_name),
                Email = profile.Email,
                UserName = profile.Email,
                Source = Constants.Linkedin,
                PictureUrl = profile.Picture,
                PlatformId = profile.UserId.ToString()                
            };
        }

    }
}
