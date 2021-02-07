using System.Collections.Generic;

namespace MarsRover.Domain.Model
{
    public class DiscoveryPlan
    {
        public Plateau Plateau { get; set; }
        public List<NavigationPlan> NavigationPlanList { get; set; }
    }
}
