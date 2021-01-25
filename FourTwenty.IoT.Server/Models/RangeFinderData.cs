using FourTwenty.IoT.Connect.Models;

namespace FourTwenty.IoT.Server.Models
{
    public class RangeFinderData : ModuleResponse
    {
        public RangeFinderData() : base() { }

        public RangeFinderData(bool isSuccess) : base(isSuccess) { }

        public RangeFinderData(bool isSuccess, double distance) : base(isSuccess)
        {
	        Distance = distance;
        }

        public double Distance { get;set;}
    }
}
