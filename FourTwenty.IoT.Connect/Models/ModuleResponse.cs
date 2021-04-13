using System;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.Models
{
	public class ModuleResponse
	{
		public bool IsSuccess { get; set; }
		public IData Data { get; set; }

		public ModuleResponse(bool isSuccess, IData data)
		{
			IsSuccess = isSuccess;
			Data = data;
		}
	}

	public class ModuleResponseEventArgs : EventArgs
	{
		public ModuleResponse Data { get; }
        
		public ModuleResponseEventArgs(ModuleResponse data)
		{
			Data = data;
		}
	}
}
