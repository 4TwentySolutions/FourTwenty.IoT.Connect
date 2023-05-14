﻿using System;
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
        public string RawError { get; set; }

        public ModuleResponse(int moduleId, bool isSuccess, BaseData data, Exception exception = null)
		{
			IsSuccess = isSuccess;
			RawData = JsonConvert.SerializeObject(data);
            DataType = data?.GetType().Name;
            RawError = exception?.Message;
            ModuleId = moduleId;
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
