using Launcher.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SinRpLauncher.Repository
{
    public class UserRepository
    {
        /// <summary>
        /// Serialize to json and write user data(nickname and server)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        public static void WriteUserData(string path, UserAccount info)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("NickName", info.NickName); // add nick to dict for json
            data.Add("ServerID", info.Server.ToString()); // add serverID to dict for json

            string serialize_data = JsonSerializer.Serialize(data);

            using (StreamWriter sr = new StreamWriter(path))
            {
                sr.Write(serialize_data);
            }
        }


        /// <summary>
        /// Deserialize data from json data user(nickname and server)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Dictionary<string, string> GetUserData(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                Dictionary<string, string>? desirialize_data = 
                    JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);

                return desirialize_data ?? new Dictionary<string, string>();
            }
            catch (Exception)
            {
                throw; // just trust me.
            }
        }
    }
}
