using Google.Protobuf.Collections;
using POGOProtos.Data;
using POGOProtos.Data.Battle;
using POGOProtos.Data.Gym;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.PokemonModels;
using PokemonGo.RocketAPI.Helpers;
using PokemonGo.RocketAPI.Rpc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.GymModels
{
    public class Battle : BaseRpc
    {
        private Gym gym;
	    private Pokemon[] teams;
	    private List<BattlePokemonInfo> bteam = new List<BattlePokemonInfo>();
        private StartGymBattleResponse battleResponse;

	    private List<int> gymIndex = new List<int>();
            
        public bool Concluded;
        private BattleState outcome;

        /**
         * New battle to track the state of a battle.
         *
         * @param api  The api instance to submit requests with.
         * @param teams The Pokemon to use for attacking in the battle.
         * @param gym  The Gym to fight at.
         */
        public Battle(Client client, Pokemon[] teams, Gym gym) : base(client)
        {
            this.teams = teams;
            this.gym = gym;

            foreach (Pokemon team in teams)
            {
                bteam.Add(this.CreateBattlePokemon(team));
            }
        }

        /**
         * Start a battle.
         *
         * @return Result of the attempt to start
         * @throws LoginFailedException  if the login failed
         * @throws RemoteServerException When a buffer exception is thrown
         */
        public async Task<StartGymBattleResponse.Types.Result> Start()
        {
            var message = new StartGymBattleMessage
            {
                GymId = gym.GetId(),
                AttackingPokemonIds = { },
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude
            };

            foreach (Pokemon team in teams) {
                message.AttackingPokemonIds.Add(team.GetId());
		    }
            
            List<PokemonData> defenders = await gym.GetDefendingPokemon();
            message.DefendingPokemonId = defenders.ElementAt(0).Id; // may need to be sorted

            battleResponse = await PostProtoPayload<Request, StartGymBattleResponse>(RequestType.StartGymBattle, message);

            // need to send blank action
            await this.SendBlankAction();

            foreach (BattleAction action in battleResponse.BattleLog.BattleActions)
            {
                gymIndex.Add(action.TargetIndex);
            }

            return battleResponse.Result;
        }


        /**
         * Attack a gym.
         *
         * @param times the amount of times to attack
         * @return Battle
         * @throws LoginFailedException  if the login failed
         * @throws RemoteServerException When a buffer exception is thrown
         */
        public async Task<AttackGymResponse> Attack(int times)
        {
            List<BattleAction> actions = new List<BattleAction>();

            for (int i = 0; i < times; i++)
            {
                BattleAction action = new BattleAction();
                action.Type = BattleActionType.ActionAttack;
                action.DurationMs = 500;
                action.ActionStartMs = Utils.GetTime(true) + (100 * times);
                action.TargetIndex = -1;
                actions.Add(action);
            }
            return await DoActions(actions);
        }
        
	    /**
	     * Creates a battle pokemon object to send with the request.
	     *
	     * @param pokemon the battle pokemon
	     * @return BattlePokemonInfo
	     */
	    private BattlePokemonInfo CreateBattlePokemon(Pokemon pokemon)
        {
            return new BattlePokemonInfo()
            {
                CurrentEnergy = 0,
                CurrentHealth = 100,
                PokemonData = pokemon.GetDefaultInstanceForType()
            };
        }

        /**
         * Get the Pokemondata for the defenders.
         *
         * @param index of defender(0 to gym lever)
         * @return Battle
         */
        private async Task<PokemonData> GetDefender(int index)
        {
            RepeatedField<GymMembership> defenders = await gym.GetGymMembers();
            return defenders[0].PokemonData;
	    }

	    /**
	     * Get the last action from server.
	     *
	     * @return BattleAction
	     */
	    private BattleAction GetLastActionFromServer()
        {
            BattleAction action;
            int actionCount = battleResponse.BattleLog.BattleActions.Count;
            action = battleResponse.BattleLog.BattleActions[actionCount - 1];
            return action;
        }

        /**
         * Send blank action, used for polling the state of the battle. (i think).
         *
         * @return AttackGymResponse
         */
        private async Task<AttackGymResponse> SendBlankAction()
        {
            var message = new AttackGymMessage
            {
                GymId = gym.GetId(),
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude,
                BattleId = battleResponse.BattleId
            };
            
            return await PostProtoPayload<Request, AttackGymResponse>(RequestType.AttackGym, message);
        }


	    /**
	     * Do Actions in battle.
	     *
	     * @param actions list of actions to send in this request
	     * @return AttackGymResponse
	     */
	    private async Task<AttackGymResponse> DoActions(List<BattleAction> actions)
        {
            var message = new AttackGymMessage
            {
                GymId = gym.GetId(),
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude,
                BattleId = battleResponse.BattleId,
                AttackActions = { }
            };
            
		    foreach (BattleAction action in actions)
            {
			    message.AttackActions.Add(action);
		    }

            AttackGymResponse response = await PostProtoPayload<Request, AttackGymResponse>(RequestType.AttackGym, message);
            
			if (response.BattleLog.State == BattleState.Defeated
					|| response.BattleLog.State == BattleState.Victory
					|| response.BattleLog.State == BattleState.TimedOut) {
				Concluded = true;
			}

            outcome = response.BattleLog.State;
            
            return response;
	    }

    }

}