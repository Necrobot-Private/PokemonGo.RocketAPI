using PokemonGo.RocketAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGo.RocketAPI.Hash
{
    public class LegacyHashser : IHasher
    {
        public  HashResponseContent RequestHashes(HashRequestContent request)
        {

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
