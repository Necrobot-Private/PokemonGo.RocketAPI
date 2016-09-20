using POGOProtos.Enums;
using POGOProtos.Inventory.Item;
using POGOProtos.Map.Fort;
using POGOProtos.Map.Pokemon;
using POGOProtos.Networking.Requests;
using POGOProtos.Networking.Requests.Messages;
using POGOProtos.Networking.Responses;
using PokemonGo.RocketAPI.Api.Exceptions;
using PokemonGo.RocketAPI.Api.InventoryModels;
using PokemonGo.RocketAPI.Api.MapModels.PokemonModels.EncounterModels;
using PokemonGo.RocketAPI.Api.SettingsModels;
using PokemonGo.RocketAPI.Api.Util;
using PokemonGo.RocketAPI.Rpc;
using System;
using System.Threading.Tasks;
using static POGOProtos.Networking.Responses.CatchPokemonResponse.Types;

namespace PokemonGo.RocketAPI.Api.MapModels.PokemonModels
{
    public enum EncounterKind
    {
        NORMAL,
        DISK
    }

    public class CatchablePokemon : BaseRpc, MapPoint
    {
        public String SpawnPointId;
        public ulong EncounterId;
        public PokemonId PokemonId;
        public long ExpirationTimestampMs;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        private EncounterKind encounterKind;
        private bool encountered;

        /**
	     * Instantiates a new Catchable pokemon.
	     *
	     * @param api   the api
	     * @param proto the proto
	     */
        public CatchablePokemon(Client client, MapPokemon proto) : base(client)
        {
            this.encounterKind = EncounterKind.NORMAL;
            this.SpawnPointId = proto.SpawnPointId;
            this.EncounterId = proto.EncounterId;
            this.PokemonId = proto.PokemonId;
            this.ExpirationTimestampMs = proto.ExpirationTimestampMs;
            this.Latitude = proto.Latitude;
            this.Longitude = proto.Longitude;
        }


        /**
	     * Instantiates a new Catchable pokemon.
	     *
	     * @param api   the api
	     * @param proto the proto
	     */
        public CatchablePokemon(Client client, WildPokemon proto) : base(client)
        {
            this.encounterKind = EncounterKind.NORMAL;
            this.SpawnPointId = proto.SpawnPointId;
            this.EncounterId = proto.EncounterId;
            this.PokemonId = proto.PokemonData.PokemonId;
            this.ExpirationTimestampMs = proto.TimeTillHiddenMs;
            this.Latitude = proto.Latitude;
            this.Longitude = proto.Longitude;
        }

        /**
	     * Instantiates a new Catchable pokemon.
	     *
	     * @param api   the api
	     * @param proto the proto
	     */
        public CatchablePokemon(Client client, FortData proto) : base(client)
        {
            if (proto.LureInfo == null)
            {
                throw new Exception("Fort does not have lure");
            }

            // TODO: does this work?
            // seems that spawnPoint it's fortId in catchAPI so it should be safe to just set it in that way
            this.SpawnPointId = proto.LureInfo.FortId;
            this.EncounterId = proto.LureInfo.EncounterId;
            this.PokemonId = proto.LureInfo.ActivePokemonId;
            this.ExpirationTimestampMs = proto.LureInfo.LureExpiresTimestampMs;
            this.Latitude = proto.Latitude;
            this.Longitude = proto.Longitude;
            this.encounterKind = EncounterKind.DISK;
        }

        /**
         * Encounter pokemon encounter result.
         *
         * @return the encounter result
         */
        public async Task<EncounterResult> EncounterPokemon()
        {
            if (encounterKind == EncounterKind.NORMAL)
            {
                return await EncounterNormalPokemon();
            }
            else if (encounterKind == EncounterKind.DISK)
            {
                return await EncounterDiskPokemon();
            }

            throw new Exception("Catchable pokemon missing encounter type");
        }

        /**
         * Encounter pokemon encounter result.
         *
         * @return the encounter result
         */
        public async Task<EncounterResult> EncounterNormalPokemon()
        {

            var reqMsg = new EncounterMessage
            {
                EncounterId = EncounterId,
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude,
                SpawnPointId = SpawnPointId
            };

            EncounterResponse response = await PostProtoPayload<Request, EncounterResponse>(RequestType.Encounter, reqMsg);

            encountered = response.Status == EncounterResponse.Types.Status.EncounterSuccess;
            return new NormalEncounterResult(Client, response);
        }

        /**
	     * Encounter pokemon
	     *
	     * @return the encounter result
	     */
        public async Task<EncounterResult> EncounterDiskPokemon()
        {
            var reqMsg = new DiskEncounterMessage
            {
                EncounterId = EncounterId,
                PlayerLatitude = Client.CurrentLatitude,
                PlayerLongitude = Client.CurrentLongitude,
                FortId = SpawnPointId
            };

            DiskEncounterResponse response = await PostProtoPayload<Request, DiskEncounterResponse>(RequestType.DiskEncounter, reqMsg);
            
            encountered = response.Result == DiskEncounterResponse.Types.Result.Success;
            return new DiskEncounterResult(Client, response);
        }

        /**
         * Tries to catch a pokemon (using defined {@link AsyncCatchOptions}).
         *
         * @param options the AsyncCatchOptions object
         * @return Observable CatchResult
         * @throws LoginFailedException  if failed to login
         * @throws RemoteServerException if the server failed to respond
         * @throws NoSuchItemException   the no such item exception
         */
        public async Task<CatchResult> CatchPokemon(CatchOptions options)
        {
            if (options != null)
            {
                if (options.GetRazzberries() == 1)
                {
                    CatchItemResult result = await UseItem(ItemId.ItemRazzBerry);
                    options.UseRazzberries(false);
                    options.MaxRazzberries(-1);
                }
            }
            else
            {
                options = new CatchOptions(Client);
            }

            return await CatchPokemon(options.GetNormalizedHitPosition(),
                    options.GetNormalizedReticleSize(),
                    options.GetSpinModifier(),
                    options.GetItemBall(),
                    options.GetMaxPokeballs(),
                    options.GetRazzberries());
        }

        /**
	     * Tries to catch a pokemon (will attempt to use a pokeball if the capture probability greater than 50%, if you have
	     * none will use greatball etc).
	     *
	     * @param encounter the encounter to compare
	     * @param options   the CatchOptions object
	     * @return the catch result
	     * @throws LoginFailedException     the login failed exception
	     * @throws RemoteServerException    the remote server exception
	     * @throws NoSuchItemException      the no such item exception
	     * @throws EncounterFailedException the encounter failed exception
	     */
        public async Task<CatchResult> CatchPokemon(EncounterResult encounter, CatchOptions options)
        {
            if (!encounter.WasSuccessful())
                throw new EncounterFailedException();

            double probability = encounter.GetCaptureProbability().CaptureProbability_[0];

            if (options != null)
            {
                if (options.GetUseRazzberries())
                {
                    CatchOptions asyncOptions = options;
                    Pokeball asyncPokeball = asyncOptions.GetItemBall(probability);
                    CatchItemResult result = await UseItem(ItemId.ItemRazzBerry);
                    if (!result.GetSuccess())
                    {
                        return new CatchResult();
                    }
                    return await CatchPokemon(asyncOptions.GetNormalizedHitPosition(),
                        asyncOptions.GetNormalizedReticleSize(),
                        asyncOptions.GetSpinModifier(),
                        asyncPokeball);
                }
            }
            else
            {
                options = new CatchOptions(Client);
            }
            return await CatchPokemon(options.GetNormalizedHitPosition(),
                options.GetNormalizedReticleSize(),
                options.GetSpinModifier(),
                options.GetItemBall(probability));
        }

        /**
         * Tries to catch a pokemon (will attempt to use a pokeball, if you have
         * none will use greatball etc).
         *
         * @return CatchResult
         * @throws LoginFailedException  if failed to login
         * @throws RemoteServerException if the server failed to respond
         * @throws NoSuchItemException   the no such item exception
         */
        public async Task<CatchResult> CatchPokemon()
        {
            return await CatchPokemon(new CatchOptions(Client));
	    }

        /**
         * Tries to catch a pokemon.
         *
         * @param normalizedHitPosition the normalized hit position
         * @param normalizedReticleSize the normalized hit reticle
         * @param spinModifier          the spin modifier
         * @param type                  Type of pokeball to throw
         * @param amount                Max number of Pokeballs to throw, negative number for
         *                              unlimited
         * @return CatchResult of resulted try to catch pokemon
         * @throws LoginFailedException  if failed to login
         * @throws RemoteServerException if the server failed to respond
         */
        public async Task<CatchResult> CatchPokemon(double normalizedHitPosition,
                                        double normalizedReticleSize, double spinModifier, Pokeball type,
                                        int amount)
        { 
	        return await CatchPokemon(normalizedHitPosition, normalizedReticleSize, spinModifier, type, amount, 0);
        }

        
        /**
	     * Tries to catch a pokemon.
	     *
	     * @param normalizedHitPosition the normalized hit position
	     * @param normalizedReticleSize the normalized hit reticle
	     * @param spinModifier          the spin modifier
	     * @param type                  Type of pokeball to throw
	     * @param amount                Max number of Pokeballs to throw, negative number for
	     *                              unlimited
	     * @param razberriesLimit       The maximum amount of razberries to use, -1 for unlimited
	     * @return CatchResult of resulted try to catch pokemon
	     * @throws LoginFailedException  if failed to login
	     * @throws RemoteServerException if the server failed to respond
	     */
        public async Task<CatchResult> CatchPokemon(double normalizedHitPosition,
                                        double normalizedReticleSize, double spinModifier, Pokeball type,
                                        int amount, int razberriesLimit)
        {
            int razberries = 0;
            int numThrows = 0;
            CatchResult result;
            do
            {
                if (razberries < razberriesLimit || razberriesLimit == -1)
                {
                    await UseItem(ItemId.ItemRazzBerry);
                    razberries++;
                }
                result = await CatchPokemon(normalizedHitPosition, normalizedReticleSize, spinModifier, type);
                if (result == null)
                {
                    // Log.wtf(TAG, "Got a null result after catch attempt");
                    break;
                }

                // continue for the following cases:
                // CatchStatus.CATCH_ESCAPE
                // CatchStatus.CATCH_MISSED
                // covers all cases

                // if its caught of has fleed, end the loop
                // FLEE OR SUCCESS
                if (result.GetStatus() == CatchStatus.CatchFlee || result.GetStatus() == CatchStatus.CatchSuccess)
                {
                    //Log.v(TAG, "Pokemon caught/or flee");
                    break;
                }

                // if error or unrecognized end the loop
                // ERROR OR UNRECOGNIZED
                if (result.GetStatus() == CatchStatus.CatchError) // TODO Review: In C# there is no Unrecognized.
                {
                    //Log.wtf(TAG, "Got an error or unrecognized catch attempt");
                    //Log.wtf(TAG, "Proto:" + result);
                    break;
                }

                numThrows++;
            }
            while (amount < 0 || numThrows < amount);

            return result;
        }

        /**
	     * Tries to catch a pokemon.
	     *
	     * @param normalizedHitPosition the normalized hit position
	     * @param normalizedReticleSize the normalized hit reticle
	     * @param spinModifier          the spin modifier
	     * @param type                  Type of pokeball to throw
	     * @return CatchResult of resulted try to catch pokemon
	     */
        public async Task<CatchResult> CatchPokemon(double normalizedHitPosition, double normalizedReticleSize, double spinModifier, Pokeball type)
        {
            if (!encountered)
            {
                return new CatchResult();
            }

            var reqMsg = new CatchPokemonMessage
            {
                EncounterId = EncounterId,
                HitPokemon = true,
                NormalizedHitPosition = normalizedHitPosition,
                NormalizedReticleSize = normalizedReticleSize,
                SpawnPointId = SpawnPointId,
                SpinModifier = spinModifier,
                Pokeball = (ItemId)type
            };

            CatchPokemonResponse response = await PostProtoPayload<Request, CatchPokemonResponse>(RequestType.CatchPokemon, reqMsg);

            // pokemon is caught of flees
            if (response.Status == CatchStatus.CatchFlee || response.Status == CatchStatus.CatchSuccess)
            {
                Client.ApiMap.RemoveCatchable(this);
            }

            // escapes
            if (response.Status == CatchStatus.CatchEscape)
            {
                await Client.Inventories.UpdateInventories();
            }
            CatchResult res = new CatchResult(response);
            return res;
        }
    
        /**
         * Tries to use an item on a catchable pokemon (ie razzberry).
         *
         * @param item the item ID
         * @return CatchItemResult info about the new modifiers about the pokemon (can move, item capture multi) eg
         */
        public async Task<CatchItemResult> UseItem(ItemId item)
        {
            var reqMsg = new UseItemCaptureMessage
            {
                EncounterId = EncounterId,
                SpawnPointId = SpawnPointId,
                ItemId = item
            };

            UseItemCaptureResponse response = await PostProtoPayload<Request, UseItemCaptureResponse>(RequestType.UseItemCapture, reqMsg);
            return new CatchItemResult(response);
        }
	
        public override bool Equals(Object obj)
        {
            if (obj == this)
            {
                return true;
            }
            else if (obj is CatchablePokemon)
            {
                return this.EncounterId == ((CatchablePokemon)obj).EncounterId;
            }
            return false;

        }

        public override int GetHashCode()
        {
            return (int)this.EncounterId;
        }

        /**
         * Encounter check
         *
         * @return Checks if encounter has happened
         */
        public bool IsEncountered()
        {
            return encountered;
        }

        /**
         * Return true when the catchable pokemon is a lured pokemon
         *
         * @return true for lured pokemon
         */
        public bool IsLured()
        {
            return encounterKind == EncounterKind.DISK;
        }
    }
}
