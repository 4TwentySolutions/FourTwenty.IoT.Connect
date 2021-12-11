using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Connect.Interfaces;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IJobsService
    {
        Task StartJobs(ICollection<IComponent> components);
        Task StartJobs(IComponent component);
        Task StopJobs();
        Task StopJobs(IComponent component);
        Task StopJobs(int componentId);
    }
}
