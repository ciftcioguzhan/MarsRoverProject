using MarsRover.Domain.Enums;
using MarsRover.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Service.Helpers
{
    public class DeserializerHelper : IDeserializerHelper
    {
        #region Rule
        private int InpuMinNumber = 3;
        private int InputForPlateau = 1;
        private int InputNavigationPlan = 2;
        private int NumberOfTokensForPlateauInputLine = 2;
        private int InputLineInitialPosition = 3;
        #endregion

        public DiscoveryPlan DeserializeDiscoveryPlan(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException();
            }

            List<string> inputLineList = new List<string>(input.Trim().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));

            if (!(inputLineList.Count >= InpuMinNumber && ((inputLineList.Count - InputForPlateau) % InputNavigationPlan == 0)))

            {
                throw new ArgumentException("invalid.count");
            }

            Plateau plateau = DeserializePlateauInput(inputLineList);
            List<NavigationPlan> roverNavigationPlanList = DeserializeRoverNavigationPlanList(inputLineList);

            DiscoveryPlan discoveryPlan = new DiscoveryPlan();

            discoveryPlan.Plateau = plateau;
            discoveryPlan.NavigationPlanList = roverNavigationPlanList;

            return discoveryPlan;
        }

        public Plateau DeserializePlateauInput(List<string> inputLineList)
        {
            string plateauInputLine = inputLineList[0];
            inputLineList.RemoveAt(0);

            string[] plateauCoordinate = plateauInputLine.Trim().Split(' ');

            if (plateauCoordinate.Length != NumberOfTokensForPlateauInputLine || !int.TryParse(plateauCoordinate[0], out int plateauCoordinateX) || !int.TryParse(plateauCoordinate[1], out int plateauCoordinateY))

            {
                throw new ArgumentException("invalid.line");
            }

            Plateau plateau = new Plateau { UpperRightCoordinate = new Coordinate { X = plateauCoordinateX, Y = plateauCoordinateY } };
            return plateau;
        }

        public List<NavigationPlan> DeserializeRoverNavigationPlanList(List<string> inputLineList)
        {
            List<NavigationPlan> roverNavigationPlanList = new List<NavigationPlan>();

            for (int lineIndex = 0; lineIndex < inputLineList.Count; lineIndex += 2)
            {
                string roverInitialPositionInputLine = inputLineList[lineIndex];
                Position roverInitialPosition = DeserializeRoverInitialPositionInput(roverInitialPositionInputLine);

                string roverRuleTypeSequenceInputLine = inputLineList[lineIndex + 1];
                List<RuleType> roverRuleTypeList = DeserializeRoverRuleTypeInput(roverRuleTypeSequenceInputLine);

                NavigationPlan roverNavigationPlan = new NavigationPlan
                {
                    InitialPosition = roverInitialPosition,
                    RuleTypeList = roverRuleTypeList
                };

                roverNavigationPlanList.Add(roverNavigationPlan);
            }

            return roverNavigationPlanList;
        }

        public Position DeserializeRoverInitialPositionInput(string roverInitialPositionInputLine)
        {
            string[] roverInitialPositionParameters = roverInitialPositionInputLine.Trim().Split(' ');

            if (roverInitialPositionParameters.Length != InputLineInitialPosition ||
                !int.TryParse(roverInitialPositionParameters[0], out int roverInitialPositionCoordinateX) ||
                !int.TryParse(roverInitialPositionParameters[1], out int roverInitialPositionCoordinateY) ||
                !Enum.TryParse(roverInitialPositionParameters[2], out WayType directionType))
            {
                throw new Exception("input.invalid.rover.position");
            }

            Position roverInitialPosition = new Position { Coordinate = new Coordinate { X = roverInitialPositionCoordinateX, Y = roverInitialPositionCoordinateY }, WayType = directionType };
            return roverInitialPosition;
        }

        public List<RuleType> DeserializeRoverRuleTypeInput(string roverRuleType)
        {
            List<RuleType> qeury = roverRuleType
                                .Select(x =>
                                Enum.TryParse(x.ToString(), out RuleType instructionType) ?
                                instructionType :
                                throw new ArgumentException("input.notInvalid")
                                ).ToList();

            return qeury;
        }

        public string SerializeFinal(FinalStatus finalStatus)
        {
            string value = "";

            foreach (Position finalPosition in finalStatus.FinalRoverPositionList)
            {
                value += $"{finalPosition.Coordinate.X} {finalPosition.Coordinate.Y} {finalPosition.WayType}\n";
            }

            return value;
        }
    }
}
