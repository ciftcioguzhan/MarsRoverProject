using MarsRover.Domain.Enums;

namespace MarsRover.Domain.Model
{
    public class Position
    {
        public Coordinate Coordinate { get; set; }
        public WayType WayType { get; set; }
    }
}
