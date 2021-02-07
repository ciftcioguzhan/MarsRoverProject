using MarsRover.Domain.Model;
using MarsRover.Service.Abstract;
using MarsRover.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Service.Concrete
{
    public class MarsRoverService : IMarsRoverService
    {
        private readonly IDeserializerHelper _deserializerHelper;

        public MarsRoverService()
        {

        }
        public MarsRoverService(IDeserializerHelper deserializerHelper)
        {
            this._deserializerHelper = deserializerHelper;
        }

        public string ExecuteDiscoveryPlan(string input)
        {
            if (_deserializerHelper == null)
            {
                throw new ArgumentNullException("explorationPlanDeserializer");
            }



            DiscoveryPlan discoveryPlan = _deserializerHelper.DeserializeDiscoveryPlan(input);

            FinalStatus finalStatus = this.ExecuteDiscoveryPlan(discoveryPlan);

            string output = _deserializerHelper.SerializeFinal(finalStatus);
            return output;
        }
                       
        public FinalStatus ExecuteDiscoveryPlan(DiscoveryPlan explorationPlan)
        {
            List<IRoverController> roverControllerList = new List<IRoverController>();

            foreach (NavigationPlan roverNavigationPlan in explorationPlan.NavigationPlanList)
            {
                IRoverController roverController = new RoverController();
                roverControllerList.Add(roverController);

                roverController.NavigationPlan(explorationPlan.Plateau, roverNavigationPlan);
            }

            FinalStatus finalStatus = new FinalStatus
            {
                FinalRoverPositionList = roverControllerList.Select(roverController => roverController.Position).ToList()
            };

            return finalStatus;
        }
    }
}
