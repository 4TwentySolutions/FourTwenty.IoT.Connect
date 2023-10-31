using FourTwenty.IoT.Connect.Entities;
using System;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class ModuleHistoryItemVm : EntityVm<ModuleHistoryItem>
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        //TODO it's not utc for now as it it's not DateTimeOffset If DateTimeOffset isn't working in sqlite - you can google solutions.
        /// <summary>
        /// Date in UTC
        /// </summary>
        public DateTime Date { get; set; }
        public string Data { get; set; }
        public bool IsSuccess { get; set; }
        public string DataType { get; set; }
        public string Error { get; set; }
    }
}
