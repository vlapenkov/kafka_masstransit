using KafkaMasstransitWebApp1.Events;
using MassTransit;
using System.Threading.Tasks;

namespace KafkaMasstransitWebApp1.Handlers
{
    internal class VideoDeletedEventConsumer : IConsumer<VideoDeletedEvent>
    {
        public Task Consume(ConsumeContext<VideoDeletedEvent> context)
        {
            var message = context.Message.Title;
            return Task.CompletedTask;
        }
    }
}
