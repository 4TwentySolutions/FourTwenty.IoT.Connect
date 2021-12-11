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

        public event EventHandler<ModuleResponseEventArgs> DataReceived;

        public Camera(IReadOnlyCollection<PinNameItem> pins, GpioController gpioController) : base(pins, gpioController) { }
        
        
        public ValueTask<ModuleResponse<IData>> GetPhoto()
        {
            ModuleResponse<IData> response = null;
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

                response = new ModuleResponse<IData>(true, new CameraData(fullPath));
            }
            catch (Exception ex)
            {
                response = new ModuleResponse<IData>(false, null, ex);
            }
            finally
            {
                DataReceived?.Invoke(this, new ModuleResponseEventArgs(response));
            }

            return new ValueTask<ModuleResponse<IData>>(response);
        }
    }
}
