using System;

namespace FourTwenty.IoT.Connect.Models
{
    public class ModuleResponse
    {
        public bool IsSuccess { get; set; }

        public ModuleResponse() { }

        public ModuleResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

	public class ModuleResponse<T> : ModuleResponse
	{
		public T Data { get; set; }

		public ModuleResponse(bool isSuccess, T data)
		{
			IsSuccess = isSuccess;
			Data = data;
		}

		public ModuleResponse(bool isSuccess)
		{
			IsSuccess = isSuccess;
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
