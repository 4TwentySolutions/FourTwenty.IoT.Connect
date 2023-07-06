using System;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Components.Modules;
using FourTwenty.IoT.Server.Components.Relays;
using FourTwenty.IoT.Server.Components.Sensors;

namespace FourTwenty.IoT.Server.Extensions
{
    public static class ComponentExtensions
    {
        public static DhtSensor SetDhtType(this DhtSensor sensor, DhtType dhtType)
        {
            sensor.DhtType = dhtType;
            return sensor;
        }

        public static Relay SetCloseOnInit(this Relay relay, bool closeOnInit)
        {
            relay.CloseOnInit = closeOnInit;
            return relay;
        }

        public static SoilMoistureSensor SetSensorReadType(this SoilMoistureSensor sensor, SensorReadType readType)
        {
            sensor.ReadType = readType;
            return sensor;
        }

        public static SoilMoistureSensor SetAnalogSensorReadChannel(this SoilMoistureSensor sensor, int analogSensorReadChannel)
        {
            sensor.AnalogSensorReadChannel = analogSensorReadChannel;
            return sensor;
        }

        public static PhSensor SetAnalogSensorReadChannel(this PhSensor sensor, int analogSensorReadChannel)
        {
            sensor.AnalogSensorReadChannel = analogSensorReadChannel;
            return sensor;
        }


        public static bool Subscribe(this IComponent component, EventHandler<ModuleResponseEventArgs> receivedEvent)
        {
            try
            {
                switch (component)
                {
                    case ISensor sensor:
                        sensor.DataReceived += receivedEvent;
                        break;
                    case IRelay relay:
                        relay.StateChanged += receivedEvent;
                        break;
                    case Camera camera:
                        camera.DataReceived += receivedEvent;
                        break;
                    default:
                        throw new NotImplementedException($"Subscribe for {component.ComponentType.ToString()} not implemented");

                    //TODO Implement for Mcp3008IoT (if needed)
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static bool Unsubscribe(this IComponent component, EventHandler<ModuleResponseEventArgs> receivedEvent)
        {
            try
            {
                switch (component)
                {
                    case ISensor sensor:
                        sensor.DataReceived -= receivedEvent;
                        break;
                    case IRelay relay:
                        relay.StateChanged -= receivedEvent;
                        break;
                    case Camera camera:
                        camera.DataReceived -= receivedEvent;
                        break;
                    default:
                        throw new NotImplementedException($"Subscribe for {component.ComponentType.ToString()} not implemented");

                    //TODO Implement for Mcp3008IoT (if needed)
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
