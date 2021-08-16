﻿using System;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Extensions;
using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Server.Components.Relays
{
    public class Relay : IoTComponent, IRelay
    {
        private readonly SemaphoreSlim _relayLocker = new SemaphoreSlim(1, 1);

        public IDictionary<int, RelayState> States { get; }
        public event EventHandler<ModuleResponseEventArgs> StateChanged;

        public Relay(IReadOnlyCollection<int> pins, GpioController gpioController) : base(pins, gpioController)
        {
            States = new Dictionary<int, RelayState>(pins.Select(x => new KeyValuePair<int, RelayState>(x, RelayState.Closed)));
        }

        public override void SetValue(PinValue value, int pin)
        {
            if (!Pins.Contains(pin))
                return;

            try
            {
                _relayLocker.Wait();

                base.SetValue(value, pin);
                States[pin] = value == PinValue.Low ? RelayState.Opened : RelayState.Closed;

                var data = new RelayData(pin, States[pin]);

                var dpData =  data.ApplyDisplayOptions(DisplayOptions, Type);

                StateChanged?.Invoke(this, new ModuleResponseEventArgs(new ModuleResponse(true, dpData)));
            }
            finally
            {
                _relayLocker.Release();
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            foreach (var pin in Pins)
                Gpio.SetPinMode(pin, PinMode.Output);
        }
    }
}
