using System;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Server.Extensions;
using FourTwenty.IoT.Server.Components.Sensors;
using Microsoft.Extensions.Logging;
using FourTwenty.IoT.Connect.Data;

namespace FourTwenty.IoT.Server.Components.Relays
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

        public override void SetValue(PinValue value, int pin)
        {
            if (!Pins.Contains(pin))
                return;

            try
            {
                _relayLocker.Wait();

                base.SetValue(value, pin);
                States[pin] = value.GetState();
                _logger?.LogInformation($"\n{nameof(Relay)}:\n Pin: {pin} -> {value.GetState()}");

                var data = new RelayData(pin, States[pin]);

                var dpData = data.ApplyDisplayOptions(DisplayOptions, ComponentType);

                StateChanged?.Invoke(this, new ModuleResponseEventArgs(new ModuleResponse(Id, true, dpData)));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _relayLocker.Release();
            }
        }
    }
}
