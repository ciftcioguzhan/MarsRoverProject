using MarsRover.Domain.Enums;
using MarsRover.Domain.Model;
using System.Collections.Generic;

namespace MarsRover.Service.Helpers
{
    public interface IDeserializerHelper
    {
        DiscoveryPlan DeserializeDiscoveryPlan(string input);
        Plateau DeserializePlateauInput(List<string> inputLineList);
        Position DeserializeRoverInitialPositionInput(string roverInitialPositionInputLine);
        List<RuleType> DeserializeRoverRuleTypeInput(string roverRuleTypeInput);
        List<NavigationPlan> DeserializeRoverNavigationPlanList(List<string> inputLineList);
        string SerializeFinal(FinalStatus finalStatus);

    }
}