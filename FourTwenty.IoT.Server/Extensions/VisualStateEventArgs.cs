using System;

namespace FourTwenty.IoT.Server.Extensions
{
    public class VisualStateEventArgs : EventArgs
    {
        public int ModuleId { get; set; }

        public VisualStateEventArgs() { }

        public VisualStateEventArgs(int moduleId)
        {
            ModuleId = moduleId;
        }
    }
}
