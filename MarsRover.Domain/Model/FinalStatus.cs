using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover.Domain.Model
{
    public class FinalStatus : IEquatable<FinalStatus>
    {
        public List<Position> FinalRoverPositionList { get; set; }

        public bool Equals(FinalStatus otherFinalStatus)
        {
            if (FinalRoverPositionList.Count == otherFinalStatus.FinalRoverPositionList.Count)
            {
                for (int point = 0; point < FinalRoverPositionList.Count; point++)
                {
                    if (FinalRoverPositionList[point].WayType != otherFinalStatus.FinalRoverPositionList[point].WayType ||
                        FinalRoverPositionList[point].Coordinate.X != otherFinalStatus.FinalRoverPositionList[point].Coordinate.X ||
                        FinalRoverPositionList[point].Coordinate.Y != otherFinalStatus.FinalRoverPositionList[point].Coordinate.Y)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
