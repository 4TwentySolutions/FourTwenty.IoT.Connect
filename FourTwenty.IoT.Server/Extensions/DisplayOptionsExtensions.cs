using System.Collections.Generic;
using System.Linq;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Data;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Connect.Rules;
using FourTwenty.IoT.Server.DisplayOptions;
using Newtonsoft.Json;

namespace FourTwenty.IoT.Server.Extensions
{
    public static class DisplayOptionsExtensions
    {
        public static DisplayRule GetDisplayOption(this DisplayType type)
        {
            return type switch
            {
                DisplayType.Percent => new PercentDisplayRule(),
                DisplayType.Text => new TextDisplayRule(),
                _ => null
            };
        }

        public static DisplayRule GetDisplayOption(this DisplayRuleData data, ComponentType moduleType)
        {
            DisplayRule dO = null;

            switch (data.DisplayType)
            {
                case DisplayType.Percent:
                    dO = new PercentDisplayRule();
                    if (!string.IsNullOrEmpty(data.DisplayOptionParams))
                    {
                        dO.Options = JsonConvert.DeserializeObject<PercentParams>(data.DisplayOptionParams);
                    }
                    break;
                case DisplayType.Text:
                    dO = new TextDisplayRule();
                    if (!string.IsNullOrEmpty(data.DisplayOptionParams))
                    {
                        if (moduleType == ComponentType.Relay)
                        {
                            dO.Options = JsonConvert.DeserializeObject<RelayTextParams>(data.DisplayOptionParams);
                        }
                        else
                        {
                            dO.Options = JsonConvert.DeserializeObject<TextParams>(data.DisplayOptionParams);
                        }
                    }
                    break;
            }

            if (dO != null)
            {
                dO.SortOrder = data.DisplayOrder;
                dO.IsEnabled = data.IsEnabled;
                dO.Pin = data.Pin;
            }

            return dO;
        }

        public static BaseData ApplyDisplayOptions(this BaseData data, IReadOnlyCollection<DisplayRule> options, ComponentType type)
        {
            if (data != null && options?.Count > 0)
            {
                BaseData modData = null;
                var ls = options.Where(x => x.IsEnabled).OrderBy(x => x.SortOrder);

                foreach (var displayOption in ls)
                {
                    modData = displayOption.Execute(type, modData ?? data);
                }

                return modData;
            }

            return data;
        }
    }
}
