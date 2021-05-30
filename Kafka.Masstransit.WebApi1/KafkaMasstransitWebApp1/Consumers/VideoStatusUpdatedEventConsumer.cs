using KafkaMasstransitWebApp1.Events;
using MassTransit;
using System.Threading.Tasks;

namespace KafkaMasstransitWebApp1.Handlers
{
    internal class VideoStatusUpdatedEventConsumer : IConsumer<VideoStatusUpdatedEvent>
    {
        public Task Consume(ConsumeContext<VideoStatusUpdatedEvent> context)
        {
            var message = context.Message.Title;
            return Task.CompletedTask;
        }
    }
}
