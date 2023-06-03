using System;
using FourTwenty.IoT.Connect.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FourTwenty.IoT.Connect.Entities;
using FourTwenty.IoT.Connect.Interfaces;
using FourTwenty.IoT.Connect.Models;
using System.Threading;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.Components.Modules;
using FourTwenty.IoT.Server.Components.Sensors;
using FourTwenty.IoT.Server.Extensions;
using SixLabors.ImageSharp.Formats;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class ModuleVm : EntityVm<GrowBoxModule>, IDisposable
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ComponentType ComponentType { get; set; }
        public int[] Pins { get; set; }
        //public bool GroupedModule { get; set; }

        public List<PinNameItem> PinsNames { get; set; }
        public int GrowBoxId { get; set; }
        public string AdditionalData { get; set; }
        public GrowBoxVm GrowBox { get; set; }
        public IList<ModuleRuleVm> Rules { get; set; }
        public IComponent IotComponent { get; set; }

        public Dictionary<int, string> RelayValues { get; set; }

        private readonly SemaphoreSlim _relayLocker = new SemaphoreSlim(1, 1);


        public ModuleResponse CurrentValue { get; set; }
        public string CurrentValueString { get; set; }
        public string LastValueTime { get; set; }

        #region States

        public WorkState State => IotComponent?.RulesWorkState ?? WorkState.Stopped;

        public string StateIcon => State == WorkState.Running ? "far fa-pause-circle" :
            State == WorkState.Paused ? "far fa-play-circle" : State == WorkState.Stopped ? "far fa-play-circle" : string.Empty;

        public string BadgeIcon => State == WorkState.Running ? "badge-success" :
            State == WorkState.Paused ? "badge-warning" :
            State == WorkState.Stopped ? "badge-danger" : "badge-secondary";

        #endregion

        #region Events subscription

        public void Subscribe()
        {
            switch (IotComponent)
            {
                case ISensor sensor:
                    sensor.DataReceived += OnDataReceived;
                    break;
                case IRelay relay:
                    relay.StateChanged += OnDataReceived;
                    break;
                case Camera camera:
                    camera.DataReceived += OnDataReceived;
                    break;
            }
        }

        public void Unsubscribe()
        {
            switch (IotComponent)
            {
                case ISensor sensor:
                    sensor.DataReceived -= OnDataReceived;
                    break;
                case IRelay relay:
                    relay.StateChanged -= OnDataReceived;
                    break;
                case Camera camera:
                    camera.DataReceived -= OnDataReceived;
                    break;
            }
        }


        public event EventHandler<VisualStateEventArgs> VisualStateChanged;

        private async void OnDataReceived(object? sender, ModuleResponseEventArgs e)
        {
            CurrentValue = e.Data;
            
            //await UpdateCurrentValue(e.Data.Data);

            LastValueTime = DateTime.Now.ToString("dd MMM, HH:mm");

            VisualStateChanged?.Invoke(this, new VisualStateEventArgs(Id));
        }

        public async Task UpdateCurrentValue(BaseData baseDataItem)
        {
            CurrentValueString = baseDataItem?.Value ?? string.Empty;

            switch (baseDataItem)
            {
                case RelayData relayData:
                    {
                        try
                        {
                            await _relayLocker.WaitAsync();

                            RelayValues ??= new Dictionary<int, string>();

                            var pinName = PinsNames.FirstOrDefault(x => x.Pin == relayData.Pin);
                            if (pinName != null)
                            {
                                RelayValues[relayData.Pin] = $"{pinName.Name}: {relayData.State}";
                            }
                            else
                            {
                                RelayValues[relayData.Pin] = relayData.Value;
                            }
                        }
                        finally
                        {
                            _relayLocker.Release();
                        }

                        CurrentValueString = string.Join("<br/>", RelayValues.Select(x => x.Value));
                        break;
                    }
                case SoilMoistureData soilMoistureData:
                    {
                        CurrentValueString = soilMoistureData.Value;
                        break;
                    }
                case CameraData cameraData:
                    {
                        CurrentValueString = cameraData.Value;
                        break;
                    }
            }
        }

        #endregion

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
                return;

            if (disposing)
                Unsubscribe();

            _disposedValue = true;
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
