using System;
using System.Collections.Generic;
using System.Text;
using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Models
{
    public class ModuleResponse
    {
        public bool IsSuccess { get; set; }

        public ModuleResponse()
        {

        }

        public ModuleResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class ModuleResponse<T> : ModuleResponse
    {
        public T Data { get; set; }
    }
}
