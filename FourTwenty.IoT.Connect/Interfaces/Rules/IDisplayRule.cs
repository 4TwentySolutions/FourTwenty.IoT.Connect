using FourTwenty.IoT.Connect.Constants;

namespace FourTwenty.IoT.Connect.Interfaces.Rules
{
	public interface IDisplayRule
	{
        public DisplayType DisplayType { get; set; }
		public IParams Options { get; set; }
		string Execute(string value);
		BaseData Execute(ComponentType type, BaseData baseData);
	}
}
