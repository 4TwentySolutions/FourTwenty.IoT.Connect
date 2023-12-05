using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces.Rules;
using FourTwenty.IoT.Connect.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FourTwenty.IoT.Server.Rules
{
    public class ActionRule : BaseRule, IAction
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        //private readonly IIoTRuntimeService _runtimeService;
        //private ITelegramBotService _telegramBotService;
        public IRuleData Data { get; set; }
        public ComponentType SensorType { get; }
        public ActionType ActionType { get; }


        public ActionRule(ActionRuleData data, IServiceScopeFactory serviceScopeFactory, bool isEnabled)
        {
            _serviceScopeFactory = serviceScopeFactory;
            Data = data;
            ActionType = data.ActionType;
            IsEnabled = isEnabled;
            RuleType = RuleType.Action;
        }

        public async Task Execute(object value)
        {
            if (!IsEnabled)
                return;

            if (Data is ActionRuleData data)
            {
                var moduleResponse = value as ModuleResponse;

                var responseData = moduleResponse?.RawData;

                //TODO Needs to rewrite this module. Maybe dynamic code execution flow can help us (but maybe it's too much complicated for this)
                //if (data.ActionType == ActionType.Comparison)
                //{
                //    if (moduleResponse?.IsSuccess ?? false)
                //    {
                //        switch (moduleResponse.DataType)
                //        {
                //            case :
                //                switch (data.ComparisonItem)
                //                {
                //                    case ComparisonItem.Temperature:
                //                        switch (data.ComparisonDirection)
                //                        {
                //                            case ComparisonDirection.Less when dhtValue.Temperature < data.CompareValue:
                //                            case ComparisonDirection.More when dhtValue.Temperature > data.CompareValue:
                //                                await ExecuteRuleBuJobType(data, $"{dhtValue.Temperature}°C");
                //                                break;
                //                        }

                //                        break;
                //                    case ComparisonItem.Humidity:
                //                        switch (data.ComparisonDirection)
                //                        {
                //                            case ComparisonDirection.Less when dhtValue.Humidity < data.CompareValue:
                //                            case ComparisonDirection.More when dhtValue.Humidity > data.CompareValue:
                //                                await ExecuteRuleBuJobType(data, $"{dhtValue.Humidity}%");
                //                                break;
                //                        }

                //                        break;
                //                }

                //                break;
                //            case SoilMoistureData soilValue:
                //                switch (data.ComparisonDirection)
                //                {
                //                    case ComparisonDirection.Less when soilValue.Moisture < data.CompareValue:
                //                    case ComparisonDirection.More when soilValue.Moisture > data.CompareValue:
                //                        await ExecuteRuleBuJobType(data, $"{soilValue.Moisture}%");
                //                        break;
                //                }

                //                break;
                //            case PinValueData pinValue:

                //                if (data.CompareValue == pinValue.PinValue)
                //                {
                //                    await ExecuteRuleBuJobType(data, pinValue.Value);
                //                }

                //                break;
                //        }
                //    }
                //}
                //else
                //{
                //    await ExecuteRuleBuJobType(data, responseData?.Value);
                //}
            }
        }

        private async Task ExecuteRuleBuJobType(ActionRuleData data, string value = "")
        {

            //var module = _runtimeService.GetModule(data.ModuleId);

            //switch (data.ActionJobType)
            //{
            //    case ActionJobType.Read:
            //        if (module is ISensor sensor)
            //        {
            //            if (data.Delay > 0)
            //            {
            //                await Task.Delay(TimeSpan.FromSeconds(data.Delay));
            //            }

            //            var valueData = await sensor.GetData();
            //        }

            //        break;
            //    case ActionJobType.On:
            //        if (module.ComponentType == ComponentType.Relay
            //            && data.Pin.HasValue
            //            && module is Relay relay)
            //        {
            //            relay.SetValue(PinValue.Low, data.Pin.GetValueOrDefault());

            //            if (data.Delay > 0)
            //            {
            //                await Task.Delay(TimeSpan.FromSeconds(data.Delay));
            //            }
            //        }

            //        break;
            //    case ActionJobType.Off:

            //        if (module.ComponentType == ComponentType.Relay
            //            && data.Pin.HasValue
            //            && module is Relay relay2)
            //        {
            //            if (data.Delay > 0)
            //            {
            //                await Task.Delay(TimeSpan.FromSeconds(data.Delay));
            //            }

            //            relay2.SetValue(PinValue.High, data.Pin.GetValueOrDefault());
            //        }

            //        break;

               /* case ActionJobType.SendTelegramBot:
                    if (_telegramBotService != null && value != null)
                    {
                        var message = !string.IsNullOrEmpty(data.Message) ? data.Message.Replace("%value%", value) : value;

                        await _telegramBotService.SendMessage(message);
                    }
                    break;*/
           // }
        }
    }
}
