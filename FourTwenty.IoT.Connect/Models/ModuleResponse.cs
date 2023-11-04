using System;
using FourTwenty.IoT.Connect.Data;
using Newtonsoft.Json;

namespace FourTwenty.IoT.Connect.Models
{
	public class ModuleResponse
	{
		public int ModuleId { get; set; }
        public int RuleId { get; set; }
        public bool IsSuccess { get; set; }
		public string DataType { get; set; }
		public string RawData { get; set; }

        [JsonIgnore]
		public BaseData Data { get; set; }
        public string RawError { get; set; }

        public ModuleResponse(int moduleId, bool isSuccess, BaseData data, Exception exception = null)
		{
            try
            {
                RawData = JsonConvert.SerializeObject(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            IsSuccess = isSuccess;
			
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
