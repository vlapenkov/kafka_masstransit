using KafkaMasstransitWebApp2.Events;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace KafkaMasstransitWebApp2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebAppController : ControllerBase
    {
        private readonly ITopicProducer<VideoDeletedEvent> _topicProducer;

        public WebAppController(ITopicProducer<VideoDeletedEvent> topicProducer)
        {
            _topicProducer = topicProducer;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost("{title}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> PostAsync(string title)
        {
            // Another way to access the _topicProducer
            // var _topicProducer = HttpContext.RequestServices.GetRequiredService<ITopicProducer<VideoDeletedEvent>>();

            await _topicProducer.Produce(new VideoDeletedEvent
            {
                Title = $"{nameof(VideoDeletedEvent)}: {title}"
            });

            return Ok(title);
        }
    }
}
