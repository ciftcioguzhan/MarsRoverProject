using MarsRover.Domain.Model;

namespace MarsRover.Service.Abstract
{
    public interface IRoverController
    {
        Position Position { get; set; }
        Plateau Plateau { get; set; }
        void NavigationPlan(Plateau plateau, NavigationPlan navigationPlan);
        void TurnLeft();
        void TurnRight();
        void MoveForward();
    }
}
