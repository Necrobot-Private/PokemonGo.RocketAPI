using POGOProtos.Data.Capture;
using POGOProtos.Enums;
using POGOProtos.Networking.Responses;
using System.Collections.Generic;
using static POGOProtos.Networking.Responses.CatchPokemonResponse.Types;

namespace PokemonGo.RocketAPI.Api.MapModels.PokemonModels
{
    public class CatchResult
    {
        private CaptureAward captureAward;
        private CatchPokemonResponse response;
        private CatchStatus status;
        
        private bool failed;

        public CatchResult()
        {
            failed = true;
        }

        public CatchResult(CatchPokemonResponse response)
        {
            this.captureAward = response.CaptureAward;
            this.response = response;
        }

        /**
         * Gets a status from response object, or a set one if set
         *
         * @return catch status
         */
        public CatchStatus GetStatus()
        {
            if (this.response != null)
            {
                return response.Status;
            }
            return status;
        }

        public double GetMissPercent()
        {
            return response.MissPercent;
        }

        public ulong GetCapturedPokemonId()
        {
            return response.CapturedPokemonId;
        }

        public IList<ActivityType> GetActivityTypeList()
        {
            return captureAward.ActivityType;
        }

        public IList<int> GetXpList()
        {
            return captureAward.Xp;
        }

        public IList<int> GetCandyList()
        {
            return captureAward.Candy;
        }

        public IList<int> GetStardustList()
        {
            return captureAward.Stardust;
        }

        public void SetStatus(CatchStatus status)
        {
            this.status = status;
        }

        /**
         * Returns whether the catch failed.
         *
         * @return the bool
         */
        public bool IsFailed()
        {
            if (response == null)
            {
                return failed;
            }
            return (this.GetStatus() != CatchStatus.CatchSuccess || failed);
        }
    }
}
