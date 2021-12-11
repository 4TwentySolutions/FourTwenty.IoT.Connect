using System;
using System.Collections.Generic;
using System.Text;
using FourTwenty.IoT.Connect.Entities;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class GrowBoxViewModel : GrowBox
    {
        public string PortStr
        {
            get => Port.ToString();
            set
            {
                if (string.IsNullOrEmpty(value))
                    Port = null;
                if (int.TryParse(value, out int port))
                    Port = port;
            }
        }
    }
}
