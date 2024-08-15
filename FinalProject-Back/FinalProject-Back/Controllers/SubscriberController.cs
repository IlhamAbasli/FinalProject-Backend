using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Subscriber;
using Service.Services.Interfaces;

namespace FinalProject_Back.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;
        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe([FromQuery]SubscriberCreateDto request)
        {
            await _subscriberService.Create(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Unsubscribe([FromQuery]SubscriberDeleteDto request)
        {
            await _subscriberService.Delete(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriber([FromQuery] string subscriberId)
        {
            return Ok(await _subscriberService.GetSubscriber(subscriberId));
        }
    }
}
