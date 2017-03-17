using PokemonGo.RocketAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Hash
{
    public class LegacyHashser   : IHasher
    {
        public async Task<HashResponseContent> RequestHashesAsync(HashRequestContent request)
        {
            await Task.Delay(0).ConfigureAwait(false); // Just to get rid of warning. Remove this line if the below code uses async calls.

            var hashed = new HashResponseContent()
            {
                LocationAuthHash =  Utils.GenerateLocation1(request.AuthTicket, BitConverter.Int64BitsToDouble(request.Latitude64), BitConverter.Int64BitsToDouble(request.Longitude64), BitConverter.Int64BitsToDouble(request.Accuracy64)),
                LocationHash = Utils.GenerateLocation2(BitConverter.Int64BitsToDouble(request.Latitude64), BitConverter.Int64BitsToDouble(request.Longitude64), BitConverter.Int64BitsToDouble(request.Accuracy64)),
                RequestHashes = new List<long>()
            };
            foreach (var req in request.Requests)
                hashed.RequestHashes.Add((long)Utils.GenerateRequestHash(request.AuthTicket, req));

            return hashed;
        }
    }
}
