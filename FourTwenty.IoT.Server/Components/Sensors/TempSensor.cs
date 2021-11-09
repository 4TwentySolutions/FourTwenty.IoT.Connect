using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class TempSensor : IoTComponent, ISensor
    {
        public event EventHandler<ModuleResponseEventArgs> DataReceived;

        public TempSensor(IReadOnlyCollection<int> pins, GpioController gpioController) : base(pins, gpioController) {}

        public TempSensor(IReadOnlyCollection<IRule> rules, IReadOnlyCollection<int> pins, GpioController gpioController) : base(rules, pins, gpioController) {}

        public override void Initialize() {}

        public ValueTask<object> GetData()
        {
            var value = string.Empty;
            ModuleResponse response = new ModuleResponse(false, null);

            var rawValue = GetSensorData();

            if (!string.IsNullOrEmpty(rawValue))
            {
                var index = rawValue.IndexOf("t=");
                if (index > 0)
                {
                    var val = rawValue.Remove(0, index + 2);
                    if (!string.IsNullOrEmpty(val) && int.TryParse(val, out var intValue))
                    {
                        response = new ModuleResponse(true, new TempData(intValue / 1000d));
                    }
                }
            }

            DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));

            return new ValueTask<object>(response);
        }


        private string GetSensorData()
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cat",  //my linux command i want to execute
                    Arguments = "/sys/bus/w1/devices/28-0300a279b8c6/w1_slave",  //the argument to return ip address
                    UseShellExecute = false,
                    RedirectStandardOutput = true,  //redirect output to my code here
                    CreateNoWindow = true //do not show a window
                }
            };

            proc.Start();  //start the process
            var lines = new List<string>();
            while (!proc.StandardOutput.EndOfStream)  //wait until entire stream from output read in
            {
                lines.Add(proc.StandardOutput.ReadLine()); //this contains the ip output      
            }

            return string.Join("", lines);
        }
    }
}
