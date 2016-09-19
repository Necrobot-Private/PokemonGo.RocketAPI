using POGOProtos.Networking.Responses;

namespace PokemonGo.RocketAPI.Api.MapModels.PokemonModels
{
    public class CatchItemResult
    {
        private UseItemCaptureResponse proto;

        public CatchItemResult(UseItemCaptureResponse proto)
        {
            this.proto = proto;
        }

        public bool GetSuccess()
        {
            return proto.Success;
        }

        public double GetItemCaptureMult()
        {
            return proto.ItemCaptureMult;
        }

        public double GetItemFleeMult()
        {
            return proto.ItemFleeMult;
        }

        public bool GetStopMovement()
        {
            return proto.StopMovement;
        }

        public bool GetStopAttack()
        {
            return proto.StopAttack;
        }

        public bool GetTarGetMax()
        {
            return proto.TargetMax;
        }

        public bool GetTarGetSlow()
        {
            return proto.TargetSlow;
        }
    }

}
