using System;
using System.Collections.Generic;
using System.Text;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Server.Models
{
	public class WaterTankData : ModuleResponse
	{
		public double Value { get; set; }
		public string ValueLine { get; set; }
	}
}
