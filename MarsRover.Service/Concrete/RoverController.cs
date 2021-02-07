using MarsRover.Domain.Enums;
using MarsRover.Domain.Model;
using MarsRover.Service.Abstract;
using System;

namespace MarsRover.Service.Concrete
{
    public class RoverController : IRoverController
    {
        public Position Position { get; set; }
        public Plateau Plateau { get; set; }

        public void TurnLeft()
        {
            switch (Position.WayType)
            {
                case WayType.West:
                    Position.WayType = WayType.South;
                    break;
                case WayType.South:
                    Position.WayType = WayType.East;
                    break;
                case WayType.North:
                    Position.WayType = WayType.West;
                    break;
                case WayType.East:
                    Position.WayType = WayType.North;
                    break;
            }
        }

        public void TurnRight()
        {
            switch (Position.WayType)
            {
                case WayType.South:
                    Position.WayType = WayType.West;
                    break;
                case WayType.West:
                    Position.WayType = WayType.North;
                    break;
                case WayType.East:
                    Position.WayType = WayType.South;
                    break;
                case WayType.North:
                    Position.WayType = WayType.East;
                    break;
            }
        }
        public void MoveForward()
        {
            ValidateCoordinate(Position.Coordinate);

            Coordinate coordinate = new Coordinate
            {
                X = Position.Coordinate.X,
                Y = Position.Coordinate.Y
            };

            switch (Position.WayType)
            {
                case WayType.East:
                    coordinate.X++;
                    break;
                case WayType.North:
                    coordinate.Y++;
                    break;
                case WayType.West:
                    coordinate.X--;
                    break;
                case WayType.South:
                    coordinate.Y--;
                    break;
                default:
                    break;
            }

            ValidateCoordinate(coordinate);
            Position.Coordinate = coordinate;
        }

        private void ValidateCoordinate(Coordinate coordinate)
        {
            if (!(coordinate.X >= Plateau.LowerLefttCoordinate.X &&
                coordinate.X <= Plateau.UpperRightCoordinate.X &&
                coordinate.Y >= Plateau.LowerLefttCoordinate.Y &&
                coordinate.Y <= Plateau.UpperRightCoordinate.Y))
            {
                throw new ArgumentException();
            }
        }

  
        public void NavigationPlan(Plateau plateau, NavigationPlan navigationPlan)
        {
            this.Plateau = plateau;
            this.Position = navigationPlan.InitialPosition;

            foreach (RuleType instructionType in navigationPlan.RuleTypeList)
            {
                switch (instructionType)
                {
                    case RuleType.Move:
                        this.MoveForward();
                        break;
                    case RuleType.Left:
                        this.TurnLeft();
                        break;
                    case RuleType.Right:
                        this.TurnRight();
                        break;
                }
            }
        }
    }
}
