using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Device.Spi;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;
using Iot.Device.Adc;
using Iot.Device.Spi;
using Newtonsoft.Json;

namespace FourTwenty.IoT.Server.Components.Modules
{
    public class Mcp3008IoT : IoTComponent
    {
        // private SpiDevice _spiDevice;
        // private Mcp3008 _mcp3008;
        private readonly List<string> _readLines = new List<string>();

        public Mcp3008IoT(IReadOnlyCollection<PinNameItem> pins, GpioController gpioController) : base(pins, gpioController) { }


        public override void Initialize()
        {
            // var clk = PinsNames.FirstOrDefault(x => x.Name.ToLower().Contains("clk"));
            // var sdi = PinsNames.FirstOrDefault(x => x.Name.ToLower().Contains("mosi"));
            // var sdo = PinsNames.FirstOrDefault(x => x.Name.ToLower().Contains("miso"));
            // var cs = PinsNames.FirstOrDefault(x => x.Name.ToLower().Contains("ce"));
            //
            // if (clk == null || sdi == null || sdo == null || cs == null)
            //     return;
        }

        public async Task<List<ADCData>> ReadChannel(int channel)
        {
            try
            {
                _readLines.Clear();

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "python3",  //my linux command i want to execute
                        Arguments = $"Scripts/mcp3008-read.py --channel {channel}",  //the argument
                        UseShellExecute = false,
                        RedirectStandardOutput = true,  //redirect output to my code here
                        RedirectStandardError = true,
                        CreateNoWindow = true //do not show a window
                    }
                };

                proc.EnableRaisingEvents = true;
                proc.OutputDataReceived += ProcOnOutputDataReceived;
                proc.Start();
                proc.BeginOutputReadLine();
                proc.WaitForExit(6000);
                proc.OutputDataReceived -= ProcOnOutputDataReceived;
                var result = new List<ADCData>();
                var lines = new List<string>(_readLines);

                if (lines.Any())
                {
                    result.AddRange(lines.Select(JsonConvert.DeserializeObject<ADCData>));
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void ProcOnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            _readLines.Add(e.Data);
        }
    }

    public class ADCData
    {
        public int Channel { get; set; }
        public double Value { get; set; }
        public double Voltage { get; set; }
    }

}
