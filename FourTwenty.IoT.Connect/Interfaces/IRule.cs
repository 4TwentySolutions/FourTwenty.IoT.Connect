using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FourTwenty.IoT.Connect.Interfaces
{
    public interface IRule
    {
        int Id { get; set; }
	    bool IsEnabled { get; set; }
	    int? Pin { get; set; }
        IDictionary<string, object> Properties { get; }
        Task Execute();
        Task Stop();
        //Task PauseRulesExecution();
        //Task StopRulesExecution();
        //Task ResumeRulesExecution();
    }

    public interface IPeriodRule : IRule
    {
        TimeSpan Period { get; }
    }
}
