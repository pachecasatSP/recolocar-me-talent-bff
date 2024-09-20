using Microsoft.AspNetCore.Mvc;
using re.colocar.me.talent.Domain.Interfaces.Repositories;

namespace re.colocar.me.talent.Controllers
{
    [ApiController]
    [Route("notification")]
    // [Authorize]
    public class NotificationController : ControllerBase{
        private INotificationRepository _repository;

        public NotificationController(INotificationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{owner}/count")]
        public IActionResult GetNotificationCount(Guid owner){
            var count =_repository.GetCountByOwner(owner) ;

            return Ok(count);
        }

         [HttpGet("{owner}/all")]
        public IActionResult GetNotificationList(Guid owner){
            var result =_repository.ListAll();

            return Ok(result?.ToList());
        }
    }

}