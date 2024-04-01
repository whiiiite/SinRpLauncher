
using CDialogWindow;
using Launcher.Classes;
using Launcher.Extentions;
using Launcher.Handlers;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;

namespace Launcher.Classes
{
    /// <summary>
    /// Class for help working with http
    /// <para>Also contains some data aboute servers of launcher</para>
    /// </summary>
    public static class HttpHelper
    {
        public const string adminNode = "admin";
        public const string aboutHtmPage = "about.htm";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToXml"></param>
        /// <returns>Data about server http url and max players</returns>
        public static (string http, string maxplayers) GetHttpServerData(string pathToXml, int serverIndexInXml)
        {
            string http = string.Empty;
            string max_players = string.Empty;
            string xmlFormatText = File.ReadAllText(pathToXml); // read xml from file
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlFormatText);                 // load document with xml text

            XmlNodeList rootNodeList = xmlDocument.GetElementsByTagName("root"); // get root node

            for (int i = 0; i < rootNodeList.Count; i++)
            {
                XmlNodeList mainNodeList = rootNodeList[i].ChildNodes;                  // get child nodes from root node
                XmlNodeList dataServerNode = mainNodeList[serverIndexInXml].ChildNodes; // get http info about server from xml (first node)
                http = dataServerNode[0].InnerText;                                     // get http server url
                max_players = dataServerNode[1].InnerText;
            }

            if (http[^1] != '/') // correction url ^1 - last index
                http += '/';

            return (http, max_players);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>String with response from server including html</returns>
        public static string GetReponseStringFromServer(string url, string? login = null, string? password = null)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.Timeout = new TimeSpan(0, 0, 0, 0, 2500);

                if (login != null && password != null)
                {
                    byte[] credentialsAuth = new UTF8Encoding().GetBytes(login + ":" + password);
                    // set auth header login and password
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(credentialsAuth));
                }

                string res = httpClient.GetStringAsync(url).Result;

                return res;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
