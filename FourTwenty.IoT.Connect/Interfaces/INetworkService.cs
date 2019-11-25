using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface INetworkService
    {
        void StartNetwork(string ip, int? port);
        void StartTcp(IPAddress ipAddress, int port);
    }
}
