using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IBackgroundTaskQueue<T>
    {
        ValueTask QueueBackgroundWorkItemAsync(T workItem);
        ValueTask<T> DequeueAsync(CancellationToken cancellationToken);
        string GetQueueName();
    }
}
