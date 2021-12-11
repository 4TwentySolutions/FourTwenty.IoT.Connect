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
using FourTwenty.IoT.Server.Extensions;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class ModuleVm : EntityViewModel<GrowBoxModule>, IDisposable
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
        public GrowBoxViewModel GrowBox { get; set; }
        public ICollection<ModuleRuleVm> Rules { get; set; }
        public IComponent IotComponent { get; set; }
        public ISensor Sensor => IotComponent as ISensor;
        public IRelay Relay => IotComponent as IRelay;

        public Dictionary<int, string> RelayValues { get; set; }

        private readonly SemaphoreSlim _relayLocker = new SemaphoreSlim(1, 1);

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

            if (Sensor != null)
                Sensor.DataReceived += OnDataReceived;
            if (Relay != null)
                Relay.StateChanged += OnDataReceived;
        }

        public void Unsubscribe()
        {
            if (Sensor != null)
                Sensor.DataReceived -= OnDataReceived;
            if (Relay != null)
                Relay.StateChanged -= OnDataReceived;
        }


        public event EventHandler<VisualStateEventArgs> VisualStateChanged;

        private async void OnDataReceived(object? sender, ModuleResponseEventArgs e)
        {
            if (e.Data?.IsSuccess == false)
                return;

            await UpdateCurrentValue(e.Data?.Data);
            LastValueTime = DateTime.Now.ToString("dd MMM, HH:mm");

            VisualStateChanged?.Invoke(this, new VisualStateEventArgs());
        }

        public async Task UpdateCurrentValue(IData dataItem)
        {
            CurrentValueString = dataItem?.Value ?? string.Empty;

            switch (dataItem)
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
                        CurrentValueString = soilMoistureData.Value.ToString();
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
