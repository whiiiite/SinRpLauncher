using Launcher.Classes;
using System.Collections.Generic;
using System;

namespace SinRpLauncher.Repository
{
    public class MTAServerRepository
    {
        public MTAServer GetMTAServer(string path, int index)
        {
            (string ipServer, uint portServer) = GetServerData(path, index);
            return new MTAServer(ipServer, portServer);
        }


        private (string ip, uint port) GetServerData(string path, int index)
        {
            string pathDataSevers = path;
            Dictionary<string, string[]> serversData = Utils.GetAllItemsFromJson<string, string[]>(pathDataSevers);
            string[] dataServer = serversData["Server " + (index + 1).ToString()];
            string ip = dataServer[0]; // get ip
            uint port = Convert.ToUInt32(dataServer[1]); // get port

            return (ip, port);
        }
    }
}
