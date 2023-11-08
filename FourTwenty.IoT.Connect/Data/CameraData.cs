
namespace FourTwenty.IoT.Connect.Data
{
    public class CameraData : BaseData
    {
        public string Path;

        /// <summary>
        /// Path to the file 
        /// </summary>
        public override string Value => Path;

        public CameraData(string path)
        {
            Path = path;
        }
    }

}
