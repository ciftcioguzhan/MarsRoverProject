using MarsRover.Domain.Model;

namespace MarsRover.Service.Abstract
{
    public interface IMarsRoverService
    {
        string ExecuteDiscoveryPlan(string input);
        FinalStatus ExecuteDiscoveryPlan(DiscoveryPlan discoveryPlan);
    }    
}
