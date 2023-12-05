using GrowIoT.MessageQueue.Options;
using GrowIoT.MessageQueue.Pool;
using GrowIoT.MessageQueue.Publisher;

namespace FourTwenty.IoT.Server.Publishers
{
    public class ReminderPublisher : BasicPublisher<int>
    {
        public ReminderPublisher(ChannelPool channelPool) : base(new RabbitMqQueueOptions(channelPool, "amq.direct", "direct", "reminders", "reminders_queue"))
        {
            Init();
        }
    }
}
