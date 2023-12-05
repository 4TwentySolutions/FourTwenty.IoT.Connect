using FourTwenty.IoT.Connect.Models;
using GrowIoT.MessageQueue.Options;
using GrowIoT.MessageQueue.Pool;
using GrowIoT.MessageQueue.Publisher;

namespace FourTwenty.IoT.Server.Publishers
{
    public class HistoryPublisher : BasicPublisher<ModuleResponse>
    {
        public HistoryPublisher(ChannelPool channelPool) : base(new RabbitMqQueueOptions(channelPool, "amq.direct", "direct", "history", "history_queue"))
        {
            Init();
        }
    }
}
