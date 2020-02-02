using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FourTwenty.IoT.Server.Hubs
{
    public interface IRealTimeDataHub
    {

        // here place some method(s) for message from server to client
        Task SendData<T>(T message);
    }
    public class RealTimeDataHub : Hub<IRealTimeDataHub>
    {
        //public async Task Subscribe()
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, providerId.ToString(), CancellationToken.None);
        //}

        //public async Task Unsubscribe(int providerId)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, providerId.ToString(), CancellationToken.None);
        //}

        #region OnConnectedAsync
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("--- User Connected ---");
            await base.OnConnectedAsync();
        }
        #endregion

        #region OnDisconnectedAsync
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("--- User Disconnected ---");
            await base.OnDisconnectedAsync(exception);
        }
        #endregion
    }
}
