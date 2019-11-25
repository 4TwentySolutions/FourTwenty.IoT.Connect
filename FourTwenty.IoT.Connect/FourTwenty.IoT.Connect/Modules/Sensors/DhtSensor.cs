using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using GrowIoT.Interfaces.Sensors;
using GrowIoT.Models;

namespace GrowIoT.Modules.Sensors
{
    public class DhtSensor : BaseModule, ISensor<ModuleResponse<DthData>>
    {
        public DhtSensor(string name, int gpioPin, List<ModuleRule> rules = null) : base(gpioPin, rules, name)
        {
            Type = ModuleType.HumidityAndTemperature;
        }

        public Task<ModuleResponse<DthData>> GetData()
        {
            throw new NotImplementedException();
        }
    }

    public class DthData
    {
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
