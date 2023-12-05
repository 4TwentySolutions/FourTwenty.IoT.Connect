using FourTwenty.IoT.Connect.Models;
using GrowIoT.MessageQueue.Consumer;
using GrowIoT.MessageQueue.Options;
using GrowIoT.MessageQueue.Pool;

namespace FourTwenty.IoT.Server.Consumers
{
    public class HistoryConsumer : BasicConsumer<ModuleResponse>
    {
        public HistoryConsumer(ChannelPool channelPool) : base(new RabbitMqQueueOptions(channelPool, "amq.direct", "direct", "history", "history_queue"))
        {
        }
    }
}
