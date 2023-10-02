using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.IO;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Data;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using Iot.Device.Media;

namespace FourTwenty.IoT.Server.Components.Modules
{
    public class Camera : IoTComponent
    {
        private readonly VideoConnectionSettings _settings = new(busId: 0, captureSize: (1920, 1080), pixelFormat: PixelFormat.JPEG);

        public event EventHandler<ModuleResponseEventArgs> DataReceived;

        public Camera(IReadOnlyCollection<PinNameItem> pins, GpioController gpioController) : base(pins, gpioController) { }
        
        
        public ValueTask<ModuleResponse> GetPhoto()
        {
           ModuleResponse response = null;
            try
            {
                using VideoDevice device = VideoDevice.Create(_settings);
                var solPath = Directory.GetCurrentDirectory();
                var dirPath = "wwwroot/CameraPhotos/";
                var id = DateTime.Now.Ticks.ToString();

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                
                var fullPath = $"CameraPhotos/{id}.jpg";

                device.Capture($"{solPath}/wwwroot/{fullPath}");

                response = new ModuleResponse(Id, true, new CameraData(fullPath));
            }
            catch (Exception ex)
            {
                response = new ModuleResponse(Id, false, null, ex);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }

            return new ValueTask<ModuleResponse>(response);
        }
    }
}
