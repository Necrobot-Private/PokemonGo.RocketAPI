using POGOProtos.Data;
using POGOProtos.Data.Gym;
using POGOProtos.Enums;
using POGOProtos.Map.Fort;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.PokemonModels;
using PokemonGo.RocketAPI.Api.Util;
using PokemonGo.RocketAPI.Rpc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Api.GymModels
{
    public class Gym : BaseRpc, MapPoint
    {
        private FortData proto;
        private GetGymDetailsResponse details;

        /**
	     * Gym object.
	     *
	     * @param api   The api object to use for requests.
	     * @param proto The FortData to populate the Gym with.
	     */
        public Gym(Client client, FortData proto) : base(client)
        {
            this.proto = proto;
            this.details = null;
        }

        public String GetId()
        {
            return proto.Id;
        }

        public double Latitude
        {
            get { return proto.Latitude; }
            set { }
        }

        public double Longitude
        {
            get { return proto.Longitude; }
            set { }
        }

        public bool GetEnabled()
        {
            return proto.Enabled;
        }

        public TeamColor GetOwnedByTeam()
        {
            return proto.OwnedByTeam;
        }

        public PokemonId GetGuardPokemonId()
        {
            return proto.GuardPokemonId;
        }

        public int GetGuardPokemonCp()
        {
            return proto.GuardPokemonCp;
        }

        public long GetPoints()
        {
            return proto.GymPoints;
        }

        public bool GetIsInBattle()
        {
            return proto.IsInBattle;
        }

        public async Task<bool> IsAttackable()
        {
            return (await this.GetGymMembers()).Count != 0;
        }

        public Battle DoBattle(Pokemon[] team)
        {
            return new Battle(Client, team, this);
        }

        private async Task<GetGymDetailsResponse> Details()
        {
            if (details == null)
            {
                var message = new GetGymDetailsMessage
                {
                    GymId = this.GetId(),
                    GymLatitude = this.Latitude,
                    GymLongitude = this.Longitude,
                    PlayerLatitude = Client.CurrentLatitude,
                    PlayerLongitude = Client.CurrentLongitude
                };

                details = await PostProtoPayload<Request, GetGymDetailsResponse>(RequestType.GetGymDetails, message);
            }

            return details;
        }

        public async Task<string> GetName()
        {
            return (await Details()).Name;
        }

        public async Task<IList<string>> GetUrlsList()
        {
            return (await Details()).Urls;
        }

        public async Task<GetGymDetailsResponse.Types.Result> GetResult()
        {
            return (await Details()).Result;
        }

        public async Task<bool> InRange()
        {
            GetGymDetailsResponse.Types.Result result = await GetResult();
            return (result != GetGymDetailsResponse.Types.Result.ErrorNotInRange);
        }

        public async Task<string> GetDescription()
        {
            return (await Details()).Description;
        }


        public async Task<Google.Protobuf.Collections.RepeatedField<GymMembership>> GetGymMembers()
        {
            return (await Details()).GymState.Memberships;
        }

        /**
	     * Get a list of pokemon defending this gym.
	     *
	     * @return List of pokemon
	     * @throws LoginFailedException  if the login failed
	     * @throws RemoteServerException When a buffer exception is thrown
	     */
        public async Task<List<PokemonData>> GetDefendingPokemon()
        {
            List<PokemonData> data = new List<PokemonData>();

            foreach (GymMembership gymMember in (await GetGymMembers()))
            {
                data.Add(gymMember.PokemonData);
            }

            return data;
        }

        /**
	     * Deploy pokemon
	     *
	     * @param pokemon The pokemon to deploy
	     * @return Result of attempt to deploy pokemon
	     * @throws LoginFailedException  if the login failed
	     * @throws RemoteServerException When a buffer exception is thrown
	     */
        public async Task<FortDeployPokemonResponse.Types.Result> DeployPokemon(Pokemon pokemon)
        {
            var reqMsg = new FortDeployPokemonMessage
            {
                FortId = GetId(),
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude,
                PokemonId = pokemon.GetId()
            };

            FortDeployPokemonResponse response = await PostProtoPayload<Request, FortDeployPokemonResponse>(RequestType.FortDeployPokemon, reqMsg);
            return response.Result;
        }
    }
}
