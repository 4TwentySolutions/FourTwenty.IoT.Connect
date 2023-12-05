using System.Collections.Generic;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Helpers
{
    public static class TypesHelper
    {
        public static Dictionary<ComponentType, string> ComponentTypeNames = new()
        {
            //{ComponentType.None, string.Empty},
            {ComponentType.Temperature, "Temperature"},
            {ComponentType.HumidityAndTemperature, "Humidity&Temperature"},
            {ComponentType.RangeFinder, "Range Finder"},
            {ComponentType.SoilMoisture, "Soil Moisture"},
            {ComponentType.Relay, "Relay"},
            {ComponentType.Camera, "Camera"},
            {ComponentType.Mcp3008, "MCP3008"},
            {ComponentType.NonContactLiquid, "Non Contact Liquid"},
            {ComponentType.PhSensor, "Ph Sensor"},

        };
    }
}
