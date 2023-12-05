using FourTwenty.IoT.Connect.Models;
using GrowIoT.MessageQueue.Consumer;
using GrowIoT.MessageQueue.Options;

namespace FourTwenty.IoT.Server.Consumers
{
    public class GeneralConsumer : BasicConsumer<ComponentJobMessage>
    {
        public GeneralConsumer(RabbitMqQueueOptions options) : base(options) { }
    }
}
