using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models.Config;
using FourTwenty.IoT.Connect.Modules;
using FourTwenty.IoT.Connect.Modules.Fans;
using FourTwenty.IoT.Connect.Modules.Relays;
using FourTwenty.IoT.Connect.Modules.Sensors;

namespace FourTwenty.IoT.Connect.Services
{
    public class ConfigService : IConfigService
    {
        public async Task<ConfigModel> GetConfig()
        {
            return GetDefaultConfig();
        }

        private ConfigModel GetDefaultConfig()
        {
            Console.WriteLine("--- Using default config ---");
            var lightName = "Box Light";
            var fanName = "Fan";
            var dhtName = "Dht Sensor";
            var twoRelayName = "Light&Fan Relay";
            var waterPump = "Water Pump";

            var dhtPin = 4;
            var fanPin = 17;
            //var lightPin = 27;
            var waterPin = 27;
            return new ConfigModel
            {
                ListeningPort = 8001,
                Modules = new List<BaseModule>
                {
                    new DhtSensor(dhtName,dhtPin,new List<ModuleRule>
                    {
                        new ModuleRule
                        {
                            Type = JobType.Read,
                            CronExpression = "0/10 0/1 * 1/1 * ? *"
                        }
                    }),
                    new TwoRelayModule(twoRelayName,waterPin,fanPin,new List<ModuleRule>()
                        {
                            new PeriodRule
                            {
                                ModuleName = waterPump,
                                Type = JobType.Period,
                                CronExpression = "0/15 0/1 * 1/1 * ? *",
                                Period = 5
                            },                          
                            new ModuleRule
                            {
                                ModuleName = lightName,
                                Type = JobType.On,
                                CronExpression = "0/1 0/1 7-20 ? * *"
                            },
                            new ModuleRule
                            {
                                ModuleName = lightName,
                                Type = JobType.Off,
                                CronExpression = "0/1 0/1 21-6 ? * *"
                            },
                            new ModuleRule
                            {
                               ModuleName = fanName,
                               Type = JobType.On,
                               CronExpression = "0/1 0-20 * ? * *"
                            },
                            new ModuleRule
                            {
                               ModuleName = fanName,
                               Type = JobType.Off,
                               CronExpression = "0/1 21-59 * ? * *"
                            }
                        })
                        //.AddSubModule(new LightModule(lightName),lightPin)
                        .AddSubModule(new WaterPumpModule(name:waterPump),waterPin)
                        .AddSubModule(new FanModule(fanName),fanPin)
                }
            };
        }
    }
}
