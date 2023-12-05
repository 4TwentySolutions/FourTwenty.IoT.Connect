using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Data;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Extensions;
using GrowIoT.MessageQueue.Base;
using Microsoft.Extensions.Logging;

namespace FourTwenty.IoT.Server.Components.Modules
{
    public class Relay : IoTComponent, IRelay
    {
        private readonly SemaphoreSlim _relayLocker = new SemaphoreSlim(1, 1);

        public IDictionary<int, RelayState> States { get; }
        public bool CloseOnInit { get; set; }
        public event EventHandler<ModuleResponseEventArgs> StateChanged;

        public Relay(IReadOnlyCollection<PinNameItem> pins, GpioController gpioController) : base(pins, gpioController)
        {
            States = new Dictionary<int, RelayState>(pins.Select(x => new KeyValuePair<int, RelayState>(x.Pin, RelayState.Closed)));
        }

        public override ValueTask Initialize()
        {
            if (Pins == null || !Pins.Any() || Gpio == null)
                return ValueTask.CompletedTask;

            foreach (var pin in Pins)
            {
                if (Gpio.IsPinOpen(pin))
                {
                    Gpio.ClosePin(pin);
                }

                Gpio.OpenPin(pin);
                Gpio.SetPinMode(pin, PinMode.Output);

                if (CloseOnInit)
                {
                    SetValue(PinValue.High, pin);
                }
            }

            return ValueTask.CompletedTask;
        }


        protected override async void OnMessageConsumerReceived(object sender, MessageEventArgs<ComponentJobMessage> e)
        {
            base.OnMessageConsumerReceived(sender, e);

            ModuleResponse response = null;

            _logger?.LogInformation($"\n{nameof(Relay)}:\n Received Job Message");

            switch (e.Message.Command)
            {
                case Commands.On when e.Message.Pin.HasValue:
                    response = SetRelayValue(PinValue.Low, e.Message.Pin.GetValueOrDefault());
                    break;
                case Commands.On:
                    {
                        foreach (var pin in Pins)
                        {
                            response = SetRelayValue(PinValue.Low, pin);
                        }

                        break;
                    }
                case Commands.Off when e.Message.Pin.HasValue:
                    response = SetRelayValue(PinValue.High, e.Message.Pin.GetValueOrDefault());
                    break;
                case Commands.Off:
                    {
                        foreach (var pin in Pins)
                        {
                            response = SetRelayValue(PinValue.High, pin);
                        }

                        break;
                    }
                case Commands.Period when e.Message.Period.HasValue:
                    response = await TriggerPeriod(e.Message.Pin.GetValueOrDefault(), e.Message.Period.GetValueOrDefault(TimeSpan.Zero));
                    break;
                case Commands.Period:
                    {
                        foreach (var pin in Pins)
                        {
                            response = await TriggerPeriod(pin, e.Message.Period.GetValueOrDefault(TimeSpan.Zero));
                        }
                        break;
                    }
                case Commands.Toggle when e.Message.Pin.HasValue:
                    response = ToggleRelay(e.Message.Pin.GetValueOrDefault());
                    break;
                case Commands.Toggle:
                    {
                        foreach (var pin in Pins)
                        {
                            response = ToggleRelay(pin);
                        }
                        break;
                    }
            }

            if (response != null)
            {
                response.RuleId = e.Message.RuleId;
            }

            MessageConsumer.Ack(e.DeliveryTag);
            StateChanged?.Invoke(this, new ModuleResponseEventArgs(response));
        }
        
        public ModuleResponse SetRelayValue(PinValue value, int pin)
        {
            var result = new ModuleResponse
            {
                ModuleId = Id
            };

            if (!Pins.Contains(pin))
                return result;

            try
            {
                _relayLocker.Wait();

                base.SetValue(value, pin);
                States[pin] = value.GetState();
                _logger?.LogInformation($"\n{nameof(Relay)}:\n Pin: {pin} -> {value.GetState()}");

                var data = new RelayData(pin, States[pin]);

                var dpData = data.ApplyDisplayOptions(DisplayOptions, ComponentType);
                result.IsSuccess = true;
                result.Data = dpData;

                return result;
            }
            finally
            {
                _relayLocker.Release();
            }
        }

        private ModuleResponse ToggleRelay(int pin)
        {
            var result = new ModuleResponse
            {
                ModuleId = Id
            };

            try
            {
                _relayLocker.Wait();

                var pinValue = ReadValue(pin);
                pinValue = pinValue == PinValue.High ? PinValue.Low : PinValue.High;
                var resultData = SetValue(pinValue, pin);

                States[pin] = pinValue.GetState();
                _logger?.LogInformation($"\n{nameof(Relay)}:\n Pin: {pin} -> {pinValue.GetState()}");

                var data = new RelayData(pin, States[pin]);

                var dpData = data.ApplyDisplayOptions(DisplayOptions, ComponentType);
                result.IsSuccess = resultData;
                result.Data = dpData;

                return result;
            }
            finally
            {
                _relayLocker.Release();
            }
        }

        private async Task<ModuleResponse> TriggerPeriod(int pin, TimeSpan period)
        {
            var result = new ModuleResponse
            {
                ModuleId = Id
            };

            try
            {
                await _relayLocker.WaitAsync();

                var resultData = SetValue(PinValue.Low, pin);
                if (resultData)
                {
                    await Task.Delay(period);
                    resultData = SetValue(PinValue.High, pin);
                    States[pin] = PinValue.High.GetState();

                    _logger?.LogInformation($"\n{nameof(Relay)}:\n Pin: {pin} -> {PinValue.High.GetState()}");

                    var data = new RelayData(pin, States[pin]);
                    var dpData = data.ApplyDisplayOptions(DisplayOptions, ComponentType);
                    result.Data = dpData;
                }

                result.IsSuccess = resultData;

                return result;
            }
            finally
            {
                _relayLocker.Release();
            }
        }
    }
}
