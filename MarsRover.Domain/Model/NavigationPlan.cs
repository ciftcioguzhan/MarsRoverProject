using MarsRover.Domain.Enums;
using System.Collections.Generic;

namespace MarsRover.Domain.Model
{
    public class NavigationPlan
    {
        public Position InitialPosition { get; set; }
        public List<RuleType> RuleTypeList { get; set; }
    }
}
