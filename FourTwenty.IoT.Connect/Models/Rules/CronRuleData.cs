using System;
using CronExpressionDescriptor;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces.Rules;

namespace FourTwenty.IoT.Connect.Models
{
    public class CronRuleData : IRuleData
    {
        public string CronExpression { get; set; }
        public long? Delay { get; set; }
        public JobType Job { get; set; }

        public override string ToString()
        {
            var description = CronExpression;
            if (!string.IsNullOrEmpty(CronExpression))
            {
                try
                {
                    description = ExpressionDescriptor.GetDescription(CronExpression);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            var result = $"{Job} {description}";
            if (Delay is > 0)
            {
                result = $"{result} with {Delay} seconds delay";
            }

            return result;
        }
    }
}
