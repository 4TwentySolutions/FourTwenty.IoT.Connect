﻿using System;
using FourTwenty.IoT.Connect.Interfaces;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Interfaces.Rules;
using FourTwenty.IoT.Connect.Models;
using FourTwenty.IoT.Connect.Rules;
using FourTwenty.IoT.Server.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.Consumers;
using FourTwenty.IoT.Server.Rules;
using GrowIoT.MessageQueue.Base;
using GrowIoT.MessageQueue.Consumer;
using GrowIoT.MessageQueue.Options;
using GrowIoT.MessageQueue.Pool;

namespace FourTwenty.IoT.Server.Components
{
    public class IoTComponent : IComponent
    {
        protected readonly GpioController Gpio;

        private IServiceScopeFactory _serviceScopeFactory;
        protected IIoTRuntimeService IoTRuntimeService { get; private set; }
        protected ILogger _logger;

        protected BasicConsumer<ComponentJobMessage> MessageConsumer;
        private RabbitMqQueueOptions _rabbitMqQueueOptions;

        #region properties
        public int Id { get; set; }
        public string Name { get; set; }
        public ComponentType ComponentType { get; set; }

        public IReadOnlyCollection<CronRule> Rules { get; set; }
        public IReadOnlyCollection<DisplayRule> DisplayOptions { get; set; }
        public IReadOnlyCollection<IAction> Actions { get; set; }


        #endregion

        public IoTComponent(GpioController gpioController, ILogger logger = null) : this(null, null, gpioController, logger) { }

        public IoTComponent(IReadOnlyCollection<PinNameItem> pins, GpioController gpioController, ILogger logger = null) : this(null, pins, gpioController, logger) { }

        protected IoTComponent(IReadOnlyCollection<CronRule> rules, IReadOnlyCollection<PinNameItem> pins, GpioController gpioController, ILogger logger = null)
        {

            Gpio = gpioController;
            Pins = pins?.Select(x => x.Pin)?.ToList();
            Rules = rules;
            PinsNames = pins;
            _logger = logger;
        }

        public IReadOnlyCollection<PinNameItem> PinsNames { get; }
        public IReadOnlyCollection<int> Pins { get; }

        public bool IsInitialized { get; protected set; } = false;

        public virtual async ValueTask Initialize()
        {
            if (Pins == null || !Pins.Any() || Gpio == null || _rabbitMqQueueOptions == null)
                return;


            foreach (var pin in Pins)
            {
                if (!Gpio.IsPinOpen(pin))
                    Gpio.OpenPin(pin);
            }

            IsInitialized = true;
        }

        public virtual bool SetValue(PinValue value, int pin)
        {
            try
            {
                if (!Gpio.IsPinOpen(pin))
                    Gpio.OpenPin(pin, PinMode.Output);
                Gpio.Write(pin, value);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual PinValue ReadValue(int pin)
        {
            if (!Gpio.IsPinOpen(pin))
                Gpio.OpenPin(pin, PinMode.Input);
            return Gpio.Read(pin);
        }

        public void UpdateDisplayOptions(IEnumerable<DisplayRule> options)
        {
            DisplayOptions = new List<DisplayRule>(options);
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void SetServiceScopeFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            using var scope = _serviceScopeFactory.CreateScope();
            IoTRuntimeService = scope.ServiceProvider.GetRequiredService<IIoTRuntimeService>();
        }

        public void SetChannelPool(ChannelPool channelPool)
        {
            _rabbitMqQueueOptions = new RabbitMqQueueOptions(channelPool, "amq.direct", "direct", $"component_{Id}_jobs", $"component_{ComponentType.ToString().ToLower()}_{Id}_jobs_queue", arguments: new Dictionary<string, object> { { "x-max-priority", 10 } });
            MessageConsumer = new GeneralConsumer(_rabbitMqQueueOptions);
            MessageConsumer.Subscribe();
            MessageConsumer.OnMessageReceived += OnMessageConsumerReceived;
        }

        protected virtual void OnMessageConsumerReceived(object sender, MessageEventArgs<ComponentJobMessage> e)
        {
            //throw new NotImplementedException();
        }
    }
}
