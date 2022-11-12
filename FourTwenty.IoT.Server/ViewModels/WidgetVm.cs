using FourTwenty.IoT.Connect.Constants;
using FourTwenty.IoT.Connect.Entities;

namespace FourTwenty.IoT.Server.ViewModels
{
    public class WidgetVm : EntityViewModel<Widget>
    {
        public int Id { get; set; }
        public WidgetType WidgetType { get; set; }
        public int ModuleId { get; set; }
        public int SortOrder { get; set; }
    }
}
