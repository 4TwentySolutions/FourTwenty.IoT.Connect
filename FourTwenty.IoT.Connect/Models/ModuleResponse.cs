using System;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Connect.Models
{
	public class ModuleResponse<T> where T : BaseData
	{
		public bool IsSuccess { get; set; }
		public T Data { get; set; }
        public Exception Exception { get; set; }

        public ModuleResponse(bool isSuccess, T data, Exception exception = null)
		{
			IsSuccess = isSuccess;
			Data = data;
		}
	}

	public class ModuleResponseEventArgs : EventArgs
	{
		public ModuleResponse<BaseData> Data { get; }
        
		public ModuleResponseEventArgs(ModuleResponse<BaseData> data)
		{
			Data = data;
		}
	}
}
