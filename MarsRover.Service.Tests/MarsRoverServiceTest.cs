using MarsRover.Domain.Enums;
using MarsRover.Domain.Model;
using MarsRover.Service.Abstract;
using MarsRover.Service.Concrete;
using MarsRover.Service.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace MarsRover.Service.Tests
{
    public class MarsRoverServiceTest
    {
        private readonly IDeserializerHelper _deserializerHelper;
        private readonly IMarsRoverService _marsRoverService;

        public MarsRoverServiceTest()
        {
            _deserializerHelper = new DeserializerHelper();
            _marsRoverService = new MarsRoverService(_deserializerHelper);
        }

        #region MarsRoverServiceTest
        [Fact]
        public void MarsRoverService_ValidStringInput()
        {
            string testScenario = "5 5\n" +
                               "1 2 N\n" +
                               "LMLMLMLMM\n" +
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";

            string expectedOutput = "1 3 N\n" +
                                    "5 1 E\n";

            string actualOutput = _marsRoverService.ExecuteDiscoveryPlan(testScenario);

            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void MarsRoverService_ExecuteExplorationPlanWithInvalid()
        {
            string testInput = "5 5\n" +
                               "1 2 N\n" +
                               "LMLMLMLMMMMMM\n" +
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";

            Assert.Throws<Exception>(() => { string actualOutput = _marsRoverService.ExecuteDiscoveryPlan(testInput); });
        }

        [Fact]
        public void MarsRoverService_ValidExplorationPlanInput()
        {
            DiscoveryPlan discoveryPlan = new DiscoveryPlan
            {
                Plateau = new Plateau
                {
                    LowerLefttCoordinate = new Coordinate { X = 0, Y = 0 },
                    UpperRightCoordinate = new Coordinate { X = 5, Y = 5 }
                },

                NavigationPlanList = new List<NavigationPlan>
                {
                    new NavigationPlan
                    {
                        InitialPosition = new Position  { Coordinate = new Coordinate { X = 1, Y = 2 }, WayType = WayType.North },
                        RuleTypeList = new List<RuleType>
                        {
                            RuleType.Left,
                            RuleType.Move,
                            RuleType.Left,
                            RuleType.Move,
                            RuleType.Left,
                            RuleType.Move,
                            RuleType.Left,
                            RuleType.Move,
                            RuleType.Move,
                        }
                    },
                    new NavigationPlan
                    {
                        InitialPosition = new Position  { Coordinate = new Coordinate { X = 3, Y = 3 }, WayType = WayType.East },
                        RuleTypeList = new List<RuleType>
                        {
                            RuleType.Move,
                            RuleType.Move,
                            RuleType.Right,
                            RuleType.Move,
                            RuleType.Move,
                            RuleType.Right,
                            RuleType.Move,
                            RuleType.Right,
                            RuleType.Right,
                            RuleType.Move,
                        }
                    }
                }
            };

            FinalStatus expectedFinalStatus = new FinalStatus
            {
                FinalRoverPositionList = new List<Position>
                {
                    new Position { Coordinate = new Coordinate { X = 1, Y = 3 }, WayType = WayType.North },
                    new Position { Coordinate = new Coordinate { X = 5, Y = 1 }, WayType = WayType.East }
                }
            };

            IMarsRoverService marsRoverService = new MarsRoverService();
            FinalStatus actualFinalStatus = marsRoverService.ExecuteDiscoveryPlan(discoveryPlan);

            Assert.Equal(expectedFinalStatus, actualFinalStatus);
        }

  
        #endregion

        #region ExplorationPlanDeserializerTest
        [Fact]
        public void ExplorationPlanDeserializer_InputCountException()
        {
            string testInput = "5 5\n" +
                               "1 2 N\n" +
                               "LMLMLMLMM\n" +
                               "3 3 E\n";

            Assert.Throws<Exception>(() => { DiscoveryPlan discoveryPlan = _deserializerHelper.DeserializeDiscoveryPlan(testInput); });
        }

        [Fact]
        public void ExplorationPlanDeserializer_InputInvalidPlateauException()
        {
            string testInput = "5 E\n" +
                               "1 2 N\n" +
                               "LMLMLMLMM\n" +
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";

            Assert.Throws<Exception>(() => { DiscoveryPlan discoveryPlan = _deserializerHelper.DeserializeDiscoveryPlan(testInput); });
        }

        [Fact]
        public void ExplorationPlanDeserializer_PositionLineException()
        {
            string testInput = "5 5\n" +
                               "1 2 X\n" +
                               "LMLMLMLMM\n" +
                               "3 3 E\n" +
                               "MMRMMRMRRM\n";

            Assert.Throws<Exception>(() => { DiscoveryPlan discoveryPlan = _deserializerHelper.DeserializeDiscoveryPlan(testInput); });
        }
        #endregion
    }
}
