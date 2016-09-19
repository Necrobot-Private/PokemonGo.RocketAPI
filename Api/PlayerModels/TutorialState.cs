using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace PokemonGo.RocketAPI.Api.PlayerModels
{
    public class TutorialState
    {
        private RepeatedField<POGOProtos.Enums.TutorialState> tutorialStateList = new RepeatedField<POGOProtos.Enums.TutorialState>();

        public TutorialState(IList<POGOProtos.Enums.TutorialState> tutorialStateList)
        {
            this.tutorialStateList.AddRange(tutorialStateList);
        }

        public RepeatedField<POGOProtos.Enums.TutorialState> getTutorialStates()
        {
            return tutorialStateList;
        }

        public void addTutorialState(POGOProtos.Enums.TutorialState state)
        {
            tutorialStateList.Add(state);
        }

        public void addTutorialStates(List<POGOProtos.Enums.TutorialState> states)
        {
            tutorialStateList.AddRange(states);
        }
    }
}
