using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces
{
	public interface IDisplayOption
	{
        public bool IsEnabled { get; set; }
        public DisplayType DisplayType { get; }
		public IParams Options { get; set; }
		public int DisplayOrder { get; set; }
        public int? Pin { get; set; }
		string Execute(string value);
		IData Execute(ComponentType type, IData data);
	}

	public interface IParams { }

	public interface IRuleData { }
}
