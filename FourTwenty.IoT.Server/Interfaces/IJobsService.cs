using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IJobsService
    {
        Task StartJobs(ICollection<IModule> components);
        Task StopJobs();
    }
}
