using System;
using FourTwenty.IoT.Connect.Data;
using FourTwenty.IoT.Connect.Interfaces;
using Newtonsoft.Json;

namespace FourTwenty.IoT.Connect.Models
{
	public class ModuleResponse
	{
		public int ModuleId { get; set; }
		public bool IsSuccess { get; set; }
		public string DataType { get; set; }
		public string RawData { get; set; }
		public BaseData Data { get; set; }
        public string RawError { get; set; }

        public ModuleResponse(int moduleId, bool isSuccess, BaseData data, Exception exception = null)
		{
            IsSuccess = isSuccess;
			RawData = JsonConvert.SerializeObject(data);
            DataType = data?.GetType().FullName;
            RawError = exception?.Message;
            ModuleId = moduleId;
			Data = data;
		}

        public ModuleResponse() { }
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
