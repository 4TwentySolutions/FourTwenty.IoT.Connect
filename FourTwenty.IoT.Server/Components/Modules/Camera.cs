using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using Iot.Device.Media;

namespace FourTwenty.IoT.Server.Components.Sensors
{
    public class Camera : IoTComponent
    {
        private VideoConnectionSettings _settings = new VideoConnectionSettings(busId: 0, captureSize: (1920, 1080), pixelFormat: PixelFormat.JPEG);


        public Camera(IReadOnlyCollection<int> pins, GpioController gpioController) : base(pins, gpioController)
        {
        }

        public Camera(IReadOnlyCollection<IRule> rules, IReadOnlyCollection<int> pins, GpioController gpioController) : base(rules, pins, gpioController)
        {
        }

        public event EventHandler<ModuleResponseEventArgs> DataReceived;
        public ValueTask<object> GetData()
        {
            ModuleResponse response = null;
            try
            {
                using VideoDevice device = VideoDevice.Create(_settings);
                var path = Directory.GetCurrentDirectory();
                var dirPath = $"{path}/CameraPhotos/";
                var id = Guid.NewGuid();

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                var fullPath = $"{path}/CameraPhotos/{id}.jpg";

                device.Capture(fullPath);

                response = new ModuleResponse(true, new CameraData(fullPath));
            }
            catch (Exception ex)
            {
                response = new ModuleResponse(false, null);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }

            return new ValueTask<object>(response);
        }
    }
}
