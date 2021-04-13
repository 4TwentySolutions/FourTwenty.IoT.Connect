﻿using System.Collections.Generic;
using System.Linq;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.DisplayOptions;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using Newtonsoft.Json;

namespace FourTwenty.IoT.Connect.Extensions
{
	public static class DisplayOptionsExtensions
	{
		public static IDisplayOption GetDisplayOption(this DisplayType type)
		{
			return type switch
			{
				DisplayType.Percent => new PercentDisplayOption(),
				DisplayType.Text => new TextDisplayOption(),
				_ => null
			};
		}

		public static IDisplayOption GetDisplayOption(this DisplayRuleData data)
		{
			IDisplayOption dO = null;

			switch (data.DisplayType)
			{
				case DisplayType.Percent:
					dO =  new PercentDisplayOption();
					if (!string.IsNullOrEmpty(data.DisplayOptionParams))
					{
						dO.Options = JsonConvert.DeserializeObject<PercentParams>(data.DisplayOptionParams);
					}
					break;
				case DisplayType.Text:
					dO =  new TextDisplayOption();
					if (!string.IsNullOrEmpty(data.DisplayOptionParams))
					{
						dO.Options = JsonConvert.DeserializeObject<TextParams>(data.DisplayOptionParams);
					}
					break;
			}

			if (dO != null)
			{
				dO.DisplayOrder = data.DisplayOrder;
			}

			return dO;
		}

		public static IData ApplyDisplayOptions(this IData data, IReadOnlyCollection<IDisplayOption> options, ModuleType type)
		{
			if (data != null && options !=null && options.Count > 0)
			{
				IData modData = null;
				var ls = options.OrderBy(x => x.DisplayOrder);

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
