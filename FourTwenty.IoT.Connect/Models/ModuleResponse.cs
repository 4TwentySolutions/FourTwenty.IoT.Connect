using System;
using System.Collections.Generic;
using System.Text;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Models
{
    public class ModuleResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ModuleDataResponse<T> : ModuleResponse
    {
        public T Data { get; set; }
    }
}
