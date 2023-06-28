using System.Drawing;
using FourTwenty.IoT.Connect.Data;

namespace FourTwenty.IoT.Connect.Models
{
    public class StyledData<T> where T : BaseData
    {
        public StyledData(T data, Color color)
        {
            Data = data;
            Color = color;
        }

        public T Data { get; set; }
        public Color Color { get; set; }
    }
}
