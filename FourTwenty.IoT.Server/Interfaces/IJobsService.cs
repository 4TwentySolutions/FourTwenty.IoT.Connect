using System.Collections.Generic;
using System.Threading.Tasks;
using FourTwenty.IoT.Server.Components;

namespace FourTwenty.IoT.Server.Interfaces
{
    public interface IJobsService
    {
        Task StartJobs(ICollection<IoTComponent> components);
        Task StopJobs();
    }
}
