using FourTwenty.Core.Data.Models;
using FourTwenty.IoT.Connect.Constants;
using Newtonsoft.Json;

namespace FourTwenty.IoT.Connect.Entities
{
    public class Widget : BaseEntity<int>
    {
        public WidgetType WidgetType { get; set; }
        public int ModuleId { get; set; }
        public int SortOrder { get; set; }
    }
}
