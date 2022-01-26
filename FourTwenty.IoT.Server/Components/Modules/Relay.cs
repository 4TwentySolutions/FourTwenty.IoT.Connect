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

        public override void Initialize()
        {
            base.Initialize();
            foreach (var pin in Pins)
            {
                Gpio.SetPinMode(pin, PinMode.Output);
                if (CloseOnInit)
                {
                    SetValue(PinValue.High, pin);
                }
            }
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

                var data = new RelayData(pin, States[pin]);

                var dpData = data.ApplyDisplayOptions(DisplayOptions, ComponentType);

                StateChanged?.Invoke(this, new ModuleResponseEventArgs(new ModuleResponse<BaseData>(true, dpData)));
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
