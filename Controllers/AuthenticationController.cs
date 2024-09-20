using Microsoft.AspNetCore.Mvc;
using re.colocar.me.talent.Domain;
using re.colocar.me.talent.Domain.Interfaces.Infrastructure;
using re.colocar.me.talent.Domain.Interfaces.Repositories;
using re.colocar.me.talent.Domain.Interfaces.Services;

namespace re.colocar.me.talent.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private ILinkedinClient _linkedinClient;
        private IAuthenticationService _service;
        private IUserRepository _repository;

        public AuthenticationController(IAuthenticationService service,
                                        IUserRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Post(AuthRequest request)
        {
            
            var entity = await _service.AuthenticateAsync(request);
            return entity is not null ? Ok(entity)
                                      : BadRequest();
            
        }
      
    }
}
