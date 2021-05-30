using KafkaMasstransitWebApp1.Events;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KafkaMasstransitWebApp1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebAppController : ControllerBase
    {
        private readonly ITopicProducer<VideoStatusUpdatedEvent> _topicProducer;

        public WebAppController(ITopicProducer<VideoStatusUpdatedEvent> topicProducer)
        {
            _topicProducer = topicProducer;
        }

        [HttpPost("{title}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync(string title)
        {
            // Another way to access the _topicProducer
            // var _topicProducer = HttpContext.RequestServices.GetRequiredService<ITopicProducer<VideoStatusUpdatedEvent>>();

            await _topicProducer.Produce(new VideoStatusUpdatedEvent
            {
                Title = title
            });

            return Ok(title);
        }
    }
}
