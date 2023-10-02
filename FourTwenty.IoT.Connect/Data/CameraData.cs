
namespace FourTwenty.IoT.Connect.Data
{
    public class CameraData : BaseData
    {
        /// <summary>
        /// Path to the file 
        /// </summary>
        public CameraData(string path)
        {
            Value = path;
        }
    }

}
