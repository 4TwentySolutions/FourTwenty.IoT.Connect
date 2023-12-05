using System.Collections.Generic;
using FourTwenty.IoT.Connect.Models;
using GrowIoT.MessageQueue.Options;
using GrowIoT.MessageQueue.Pool;
using GrowIoT.MessageQueue.Publisher;

namespace FourTwenty.IoT.Server.Publishers
{
    public class JobsPublisher : BasicPublisher<ComponentJobMessage>
    {
        public JobsPublisher(ChannelPool channelPool) : base(new RabbitMqQueueOptions(channelPool, "amq.direct", "direct", string.Empty, string.Empty, arguments: new Dictionary<string, object> { { "x-max-priority", 10 } }))
        {
            Init();
        }
    }
}
