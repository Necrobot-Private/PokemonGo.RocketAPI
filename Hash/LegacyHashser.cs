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

                LocationAuthHash =  Utils.GenerateLocation1(request.AuthTicket, request.Latitude, request.Longitude, request.Altitude),
                LocationHash = Utils.GenerateLocation2(request.Latitude, request.Longitude, request.Altitude),
                RequestHashes = new List<long>()
            };
            foreach (var req in request.Requests)
                hashed.RequestHashes.Add((long)Utils.GenerateRequestHash(request.AuthTicket, req));

            return hashed;
        }
    }
}
