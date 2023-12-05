using GrowIoT.MessageQueue.Consumer;
using GrowIoT.MessageQueue.Options;
using GrowIoT.MessageQueue.Pool;

namespace FourTwenty.IoT.Server.Consumers
{
    public class ReminderConsumer : BasicConsumer<int>
    {
        public ReminderConsumer(ChannelPool channelPool) : base(new RabbitMqQueueOptions(channelPool, "amq.direct", "direct", "reminders", "reminders_queue"))
        {
        }
    }
}
