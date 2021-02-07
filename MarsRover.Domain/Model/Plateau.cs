using System;
using System.Text;

namespace MarsRover.Domain.Model
{
    public class Plateau
    {
       
        public Coordinate LowerLefttCoordinate { get; set; } = new Coordinate { X = 0, Y = 0 };
        public Coordinate UpperRightCoordinate { get; set; }
    }
}
